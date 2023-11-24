using System;
using System.Threading.Tasks;

namespace RabbitOM.Net.Rtsp.Remoting.Invokers
{
    /// <summary>
    /// Represent the request invoker manipulated by a connection object <see cref="RTSPConnection"/>
    /// </summary>
    public class RTSPInvoker : IRTSPInvoker
    {
        private readonly RTSPProxy                 _proxy      = null;

        private readonly RTSPRequestBuilder _builder    = null;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <param name="method">the method</param>
        /// <exception cref="ArgumentNullException"/>
        internal RTSPInvoker( RTSPProxy proxy , RTSPMethod method )
        {
            _proxy = proxy ?? throw new ArgumentNullException( nameof( proxy ) );

            _builder = new RTSPRequestBuilder( method , proxy.Uri );
        }




        /// <summary>
        /// Gets the syncroot
        /// </summary>
        public object SyncRoot
        {
            get => _proxy.SyncRoot;
        }

        /// <summary>
        /// Gets the builder
        /// </summary>
        protected RTSPRequestBuilder Builder
        {
            get => _builder;
        }




        /// <summary>
        /// Just make a cast to preserved fluent code just in case an invoker implementation need to be overrided
        /// </summary>
        /// <typeparam name="TRTSPInvoker">the type of the invoker</typeparam>
        /// <returns>returns an instance</returns>
        public TRTSPInvoker As<TRTSPInvoker>() where TRTSPInvoker : class, IRTSPInvoker
        {
            return this as TRTSPInvoker;
        }

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="name">the header name</param>
        /// <param name="value">the header value</param>
        /// <returns>returns the current instance</returns>
        public IRTSPInvoker AddHeader( string name , string value )
        {
            if ( _builder.CanAddHeader( name , value ) )
            {
                _builder.AddHeader( name , value );
            }

            return this;
        }

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="header">the header</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker AddHeader( RTSPHeader header )
        {
            if ( _builder.CanAddHeader( header ) )
            {
                _builder.AddHeader( header );
            }

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( bool value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( char value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( sbyte value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( byte value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( short value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( ushort value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( int value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( uint value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( long value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( ulong value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( decimal value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( float value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( double value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( DateTime value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( DateTime value , string format )
        {
            _builder.WriteBody( value , format );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( TimeSpan value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( Guid value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( string value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="format">the format</param>
        /// <param name="parameters">the parameters</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBody( string format , params object[] parameters )
        {
            _builder.WriteBody( format , parameters );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyAsBase64( string value )
        {
            _builder.WriteBodyAsBase64( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyAsBase64( byte[] value )
        {
            _builder.WriteBodyAsBase64( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine()
        {
            _builder.WriteBodyLine();

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(bool value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(char value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(sbyte value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(byte value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(short value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(ushort value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(int value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(uint value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(long value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(ulong value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(decimal value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(float value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(double value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(DateTime value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="format">the format</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(DateTime value, string format)
        {
            _builder.WriteBodyLine(value, format);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(TimeSpan value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(Guid value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(string value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="format">the format</param>
        /// <param name="parameters">the parameters</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLine(string format, params object[] parameters)
        {
            _builder.WriteBodyLine(format, parameters);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLineAsBase64(string value)
        {
            _builder.WriteBodyLineAsBase64(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRTSPInvoker WriteBodyLineAsBase64(byte[] value)
        {
            _builder.WriteBodyLineAsBase64(value);

            return this;
        }

        /// <summary>
        /// Invoke the request
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public virtual RTSPInvokerResult Invoke()
        {
            RTSPRequest  request  = CreateRequest();
            RTSPResponse response = null;

            for ( int i = 0 ; i < 3 ; ++i )
            {
                if ( ! _proxy.IsOpened )
				{
                    break;
				}

                if ( ! _proxy.RequestManager.TrySendRequest( request , out response ) || response == null )
                {
                    // Sequence is not incremented in case of retransmission
                    // See: https://datatracker.ietf.org/doc/html/rfc2326#page-50

                    continue;
                }

                if ( response.Status.Code == RTSPStatusCode.UnAuthorized )
                {
                    request = CreateRequest();

                    continue;
                }

                break;
            }

            bool succeed = false;

            if ( request != null && request.TryValidate() && response != null && response.TryValidate() )
            {
                if ( response.Status.Code == RTSPStatusCode.OK
                  || response.Status.Code == RTSPStatusCode.Continue
                  || response.Status.Code == RTSPStatusCode.Created
                   )
                {
                    succeed = true;
                }

                if ( response.Status.Code == RTSPStatusCode.UnAuthorized )
				{
                    _proxy.DispatchEvent( new RTSPAuthenticationFailedEventArgs() );
				}
            }

            return new RTSPInvokerResult( succeed , new RTSPInvokerResultRequest( request ?? RTSPRequest.CreateUnDefinedRequest() ) , new RTSPInvokerResultResponse( response ?? RTSPResponse.CreateUnDefinedResponse() ) );
        }

        /// <summary>
        /// Invoke a specific RTSP method on the remote device or computer
        /// </summary>
        /// <param name="invoker">the connection</param>
        /// <returns>returns an invoker result</returns>
        public async Task<RTSPInvokerResult> InvokeAsync()
        {
            return await Task.Run(() => Invoke());
        }

        /// <summary>
        /// Create the request
        /// </summary>
        /// <returns>returns a request object</returns>
        protected virtual RTSPRequest CreateRequest()
        {
            _builder.SequenceId = _proxy.GetNextSequenceId();

            return _builder.BuildRequest();
        }
    }
}
