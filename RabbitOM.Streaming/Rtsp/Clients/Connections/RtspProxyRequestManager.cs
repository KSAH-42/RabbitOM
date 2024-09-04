using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent a request manager
    /// </summary>
    internal sealed class RtspProxyRequestManager : IDisposable
    {
        private readonly RtspProxy                    _proxy                 = null;

        private readonly RtspChunkQueue               _chunks                = null;

        private readonly RtspMessageExtactor           _extractor               = null;

        private readonly RtspThread                   _chunkListenerThread   = null;

        private readonly RtspThread                   _requestListenerThread = null;

        private readonly RtspProxyRequestHandlerList  _requestHandlers       = null;

        private byte[]                                _buffer                = null;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspProxyRequestManager( RtspProxy proxy )
        {
            _proxy = proxy ?? throw new ArgumentNullException( nameof( proxy ) );

            _chunkListenerThread = new RtspThread( "Rtsp - proxy request manager - chunk listener" );
            _requestListenerThread = new RtspThread( "Rtsp - proxy request manager - listener " );
            _requestHandlers = new RtspProxyRequestHandlerList();
            _chunks = new RtspChunkQueue();
            _extractor = new RtspMessageExtactor();
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
            _extractor.UnInitialize();
            _requestHandlers.Clear();
        }

        /// <summary>
        /// Dispose internal resources
        /// </summary>
        public void Dispose()
        {
            Stop();
            _extractor.Dispose();
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
        public bool TrySendRequest( RtspMessageRequest request , out RtspMessageResponse response )
        {
            response = null;

            if ( request == null || ! request.TryValidate() )
            {
                return false;
            }

            var sequenceHeader = request.Headers.FindByName<RtspHeaderCSeq>( RtspHeaderNames.CSeq );

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

                var handler = new RtspProxyRequestHandler( request );

                if ( ! _requestHandlers.TryAdd( handler ) )
                {
                    return false;
                }

                using ( var scope = new RtspDisposeScope( () => _requestHandlers.Remove( handler ) ) )
                {
                    if ( ! _proxy.Send( RtspMessageSerializer.Serialize( request ) ) )
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
                        _proxy.SecurityManager.SetupAuthentication( response );
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
                int bytesReceived = _proxy.Receive( _buffer , 0 , _buffer.Length );

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
            return RtspChunkQueue.Wait( _chunks , _chunkListenerThread.ExitHandle );
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
            
            if ( _extractor.HasReachSizeLimit )
            {
                _extractor.UnInitialize();
            }

            if ( !_extractor.IsInitialized() )
            {
                _extractor.Initialize();
            }

            _extractor.PrepareWrite();
            _extractor.Write( chunk , 0 , chunk.Length );

            _extractor.PrepareRead();
            
            while ( _extractor.Read() )
            {
                if ( _extractor.IsInterleavedSequence )
                {
                    _extractor.ClearValues();

                    if ( _extractor.TryExtractInterleaved() )
                    {
                        OnDataReceived( _extractor.GetInterleavedPacket() );

                        _extractor.Discard();
                    }
                    else
                    {
                        return;
                    }
                }

                else if ( _extractor.IsMessageSequence )
                {
                    _extractor.ClearValues();

                    if ( _extractor.TryExtractResponse() )
                    {
                        OnResponseReceived( _extractor.GetResponse() );

                        _extractor.Discard();
                    }
                    else
                    {
                        return;
                    }
                }

                if ( !_extractor.HasValueProtocolChar )
                {
                    _extractor.ClearValues();
                }
            }
        }

        /// <summary>
        /// Occurs when a message has been received
        /// </summary>
        /// <param name="response">the response</param>
        private void OnResponseReceived( RtspMessageResponse response )
        {
            if ( response == null )
            {
                return;
            }

            var headerCSeq = response.Headers.FindByName<RtspHeaderCSeq>( RtspHeaderNames.CSeq );

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
        private void OnDataReceived( RtspPacket packet )
        {
            _proxy.MediaEventManager.Dispatch( new RtspPacketReceivedEventArgs( packet ) );
        }

        /// <summary>
        /// Occurs when a message has been sended
        /// </summary>
        /// <param name="message">the message</param>
        private void OnMessageSended( RtspMessage message )
        {
            _proxy.EventManager.Dispatch( new RtspMessageSendedEventArgs( message ) );
        }

        /// <summary>
        /// Occurs when a message has been received
        /// </summary>
        /// <param name="message">the message</param>
        private void OnMessageReceived( RtspMessage message )
        {
            _proxy.EventManager.Dispatch( new RtspMessageReceivedEventArgs( message ) );
        }

        /// <summary>
        /// Occurs when a error has been detected
        /// </summary>
        /// <param name="ex">the exception</param>
        private void OnError( Exception ex )
        {
            _proxy.EventManager.Dispatch( new RtspConnectionErrorEventArgs( ex ) );
        }
    }
}
