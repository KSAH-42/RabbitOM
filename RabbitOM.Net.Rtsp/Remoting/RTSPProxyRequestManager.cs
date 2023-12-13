using System;

namespace RabbitOM.Net.Rtsp.Remoting
{
    /// <summary>
    /// Represent a request manager
    /// </summary>
    internal sealed class RTSPProxyRequestManager : IDisposable
    {
        private readonly RTSPProxy                    _proxy                 = null;

        private readonly RTSPChunkQueue               _chunks                = null;

        private readonly RTSPMessageDecoder           _decoder               = null;

        private readonly RTSPThread                   _chunkListenerThread   = null;

        private readonly RTSPThread                   _requestListenerThread = null;

        private readonly RTSPProxyRequestHandlerList  _requestHandlers       = null;

        private byte[]                                _buffer                = null;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPProxyRequestManager( RTSPProxy proxy )
        {
            _proxy = proxy ?? throw new ArgumentNullException( nameof( proxy ) );

            _chunkListenerThread = new RTSPThread( "RTSP - proxy request manager - chunk listener" );
            _requestListenerThread = new RTSPThread( "RTSP - proxy request manager - listener " );
            _requestHandlers = new RTSPProxyRequestHandlerList();
            _chunks = new RTSPChunkQueue();
            _decoder = new RTSPMessageDecoder();
            _buffer = new byte[8096 * 8 * 2];
        }




        /// <summary>
        /// Start the listener
        /// </summary>
        public void Start()
        {
            _chunkListenerThread.Start( () =>
            {
                while ( _chunkListenerThread.CanContinue() )
                {
                    if ( WaitChunks() )
                    {
                        ReceiveChunks();
                    }
                }

                ReceiveChunks();
            } );

            _requestListenerThread.Start( () =>
            {
                do
                {
                    ListenMessages();
                }
                while ( _proxy.IsConnected );
            } );
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            _requestListenerThread.Stop();
            _chunkListenerThread.Stop();
            _chunks.Clear();
            _decoder.UnInitialize();
            _requestHandlers.Clear();
        }

        /// <summary>
        /// Dispose internal resources
        /// </summary>
        public void Dispose()
        {
            Stop();
            _buffer = null;
        }

        /// <summary>
        /// Send a request
        /// </summary>
        /// <param name="request">the request</param>
        /// <param name="response">the response</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        /// <para>This method try to send a request and wait a response using message correlation pattern</para>
        /// </remarks>
        public bool TrySendRequest( RTSPMessageRequest request , out RTSPMessageResponse response )
        {
            response = null;

            if ( request == null || ! request.TryValidate() )
            {
                return false;
            }

            var sequenceHeader = request.Headers.FindByName<RTSPHeaderCSeq>( RTSPHeaderNames.CSeq );

            if ( sequenceHeader == null || !sequenceHeader.TryValidate() )
            {
                return false;
            }

            try
            {
                if ( _proxy.SecurityManager.IsAuthenticationSetup() )
                {
                    _proxy.SecurityManager.ConfigureAuthorization( request );
                }

                var handler = new RTSPProxyRequestHandler( request );

                if ( ! _requestHandlers.TryAdd( handler ) )
                {
                    return false;
                }

                using ( var scope = new RTSPDisposeScope( () => _requestHandlers.Remove( handler ) ) )
                {
                    if ( ! _proxy.Send( RTSPMessageRequestSerializer.Serialize( request ) ) )
                    {
                        return false;
                    }

                    OnMessageSended( request );
                    
                    if ( ! handler.WaitCompletion( _proxy.ReceiveTimeout ) || !handler.Succeed )
                    {
                        return false;
                    }

                    if ( handler.Response == null || !handler.Response.TryValidate() )
                    {
                        return false;
                    }

                    response = handler.Response;

                    if ( ! _proxy.SecurityManager.IsAuthenticationSetup() )
                    {
                        _proxy.SecurityManager.SetupAuthentication( handler.Response );
                    }

                    OnMessageReceived( response );

                    return true;
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Cancel the pending requests
        /// </summary>
        public void CancelPendingRequests()
        {
            _requestHandlers.ForEach( handler => handler.Cancel() );
        }

        /// <summary>
        /// Receive the chunk
        /// </summary>
        private void ListenMessages()
        {
            try
            {
                var bytesReceived = _proxy.Receive( _buffer , 0 , _buffer.Length );

                if ( bytesReceived <= 0 )
                {
                    return;
                }

                var data = new byte[ bytesReceived ];

                Buffer.BlockCopy( _buffer , 0 , data , 0 , data.Length );

                _chunks.Enqueue(data);
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }
        }

        /// <summary>
        /// Wait chunk
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        private bool WaitChunks()
        {
            return RTSPChunkQueue.Wait( _chunks , _chunkListenerThread.ExitHandle );
        }

        /// <summary>
        /// Receive the chunk
        /// </summary>
        private void ReceiveChunks()
        {
            while ( _chunks.Any() )
            {
                if ( _chunks.TryDequeue( out byte[] chunk ) )
                {
                    DecodeChunk( chunk );
                }
            }
        }

        /// <summary>
        /// Handle the chunk
        /// </summary>
        /// <param name="chunk">the chunk</param>
        private void DecodeChunk( byte[] chunk )
        {
            if ( chunk == null || chunk.Length <= 0 )
            {
                return;
            }
            
            if ( _decoder.HasReachSizeLimit )
            {
                _decoder.UnInitialize();
            }

            if ( !_decoder.IsInitialized() )
            {
                _decoder.Initialize();
            }

            _decoder.PrepareWrite();
            _decoder.Write( chunk , 0 , chunk.Length );

            _decoder.PrepareRead();
            
            while ( _decoder.Read() )
            {
                if ( _decoder.IsInterleavedSequence )
                {
                    _decoder.ClearValues();

                    if ( _decoder.DecodeInterleaved() )
                    {
                        OnDataReceived( _decoder.GetInterleavedPacket() );

                        _decoder.Discard();
                    }
                    else
                    {
                        return;
                    }
                }

                else if ( _decoder.IsMessageSequence )
                {
                    _decoder.ClearValues();

                    if ( _decoder.DecodeResponse() )
                    {
                        OnResponseReceived( _decoder.GetResponse() );

                        _decoder.Discard();
                    }
                    else
                    {
                        return;
                    }
                }

                if ( !_decoder.HasValueProtocolChar )
                {
                    _decoder.ClearValues();
                }
            }
        }

        /// <summary>
        /// Occurs when a message has been received
        /// </summary>
        /// <param name="response">the response</param>
        private void OnResponseReceived( RTSPMessageResponse response )
        {
            if ( response == null )
            {
                return;
            }

            var headerCSeq = response.Headers.FindByName<RTSPHeaderCSeq>( RTSPHeaderNames.CSeq );

            if ( headerCSeq == null )
            {
                return;
            }

            var handler = _requestHandlers.FindById( headerCSeq.Value );

            if ( handler == null )
            {
                return;
            }

            handler.HandleResponse( response );
        }

        /// <summary>
        /// Occurs when data has been received
        /// </summary>
        /// <param name="packet">the packet</param>
        private void OnDataReceived( RTSPPacket packet )
        {
            if ( packet == null )
            {
                return;
            }

            _proxy.DispatchEvent( new RTSPPacketReceivedEventArgs( packet ) );
        }

        /// <summary>
        /// Occurs when a message has been sended
        /// </summary>
        /// <param name="message">the message</param>
        private void OnMessageSended( RTSPMessage message )
        {
            if ( message == null )
            {
                return;
            }

            _proxy.DispatchEvent( new RTSPMessageSendedEventArgs( message ) );
        }

        /// <summary>
        /// Occurs when a message has been received
        /// </summary>
        /// <param name="message">the message</param>
        private void OnMessageReceived( RTSPMessage message )
        {
            if ( message == null )
            {
                return;
            }

            _proxy.DispatchEvent( new RTSPMessageReceivedEventArgs( message ) );
        }

        /// <summary>
        /// Occurs when a error has been detected
        /// </summary>
        /// <param name="ex">the exception</param>
        private void OnError( Exception ex )
        {
            _proxy.DispatchEvent( new RTSPConnectionErrorEventArgs( ex ) );
        }
    }
}
