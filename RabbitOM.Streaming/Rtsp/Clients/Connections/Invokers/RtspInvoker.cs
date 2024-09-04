using System;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections.Invokers
{
    /// <summary>
    /// Represent the request invoker manipulated by a connection object <see cref="RtspConnection"/>
    /// </summary>
    public class RtspInvoker : IRtspInvoker
    {
        private readonly RtspProxy                 _proxy      = null;

        private readonly RtspMessageRequestBuilder _builder    = null;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <param name="method">the method</param>
        /// <exception cref="ArgumentNullException"/>
        internal RtspInvoker( RtspProxy proxy , RtspMethod method )
        {
            _proxy = proxy ?? throw new ArgumentNullException( nameof( proxy ) );

            _builder = new RtspMessageRequestBuilder( method , proxy.Uri );
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
        protected RtspMessageRequestBuilder Builder
        {
            get => _builder;
        }




        /// <summary>
        /// Just make a cast to preserved fluent code just in case an invoker implementation need to be overrided
        /// </summary>
        /// <typeparam name="TRtspInvoker">the type of the invoker</typeparam>
        /// <returns>returns an instance</returns>
        public TRtspInvoker As<TRtspInvoker>() where TRtspInvoker : class, IRtspInvoker
        {
            return this as TRtspInvoker;
        }

        /// <summary>
        /// Add a header
        /// </summary>
        /// <param name="name">the header name</param>
        /// <param name="value">the header value</param>
        /// <returns>returns the current instance</returns>
        public IRtspInvoker AddHeader( string name , string value )
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
        public virtual IRtspInvoker AddHeader( RtspHeader header )
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
        public virtual IRtspInvoker WriteBody( bool value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( char value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( sbyte value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( byte value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( short value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( ushort value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( int value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( uint value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( long value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( ulong value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( decimal value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( float value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( double value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( DateTime value )
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
        public virtual IRtspInvoker WriteBody( DateTime value , string format )
        {
            _builder.WriteBody( value , format );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( TimeSpan value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( Guid value )
        {
            _builder.WriteBody( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBody( string value )
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
        public virtual IRtspInvoker WriteBody( string format , params object[] parameters )
        {
            _builder.WriteBody( format , parameters );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyAsBase64( string value )
        {
            _builder.WriteBodyAsBase64( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyAsBase64( byte[] value )
        {
            _builder.WriteBodyAsBase64( value );

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine()
        {
            _builder.WriteBodyLine();

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(bool value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(char value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(sbyte value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(byte value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(short value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(ushort value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(int value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(uint value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(long value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(ulong value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(decimal value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(float value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(double value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(DateTime value)
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
        public virtual IRtspInvoker WriteBodyLine(DateTime value, string format)
        {
            _builder.WriteBodyLine(value, format);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(TimeSpan value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(Guid value)
        {
            _builder.WriteBodyLine(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLine(string value)
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
        public virtual IRtspInvoker WriteBodyLine(string format, params object[] parameters)
        {
            _builder.WriteBodyLine(format, parameters);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLineAsBase64(string value)
        {
            _builder.WriteBodyLineAsBase64(value);

            return this;
        }

        /// <summary>
        /// Write on the body
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns the current instance</returns>
        public virtual IRtspInvoker WriteBodyLineAsBase64(byte[] value)
        {
            _builder.WriteBodyLineAsBase64(value);

            return this;
        }

        /// <summary>
        /// Invoke the request
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public virtual RtspInvokerResult Invoke()
        {
            RtspMessageRequest  request  = CreateRequest();
            RtspMessageResponse response = null;

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

                if ( response.Status.Code == RtspStatusCode.UnAuthorized )
                {
                    request = CreateRequest();

                    continue;
                }

                break;
            }

            bool succeed = false;

            if ( request != null && request.TryValidate() && response != null && response.TryValidate() )
            {
                if ( response.Status.Code == RtspStatusCode.OK
                  || response.Status.Code == RtspStatusCode.Continue
                  || response.Status.Code == RtspStatusCode.Created
                   )
                {
                    succeed = true;
                }

                if ( response.Status.Code == RtspStatusCode.UnAuthorized )
                {
                    _proxy.EventManager.Dispatch( new RtspAuthenticationFailedEventArgs() );
                }
            }

            return new RtspInvokerResult( succeed , new RtspInvokerResultRequest( request ?? RtspMessageRequest.CreateUnDefinedRequest() ) , new RtspInvokerResultResponse( response ?? RtspMessageResponse.CreateUnDefinedResponse() ) );
        }

        /// <summary>
        /// Invoke a specific Rtsp method on the remote device or computer
        /// </summary>
        /// <param name="invoker">the connection</param>
        /// <returns>returns an invoker result</returns>
        public async Task<RtspInvokerResult> InvokeAsync()
        {
            return await Task.Run(() => Invoke());
        }

        /// <summary>
        /// Create the request
        /// </summary>
        /// <returns>returns a request object</returns>
        protected virtual RtspMessageRequest CreateRequest()
        {
            _builder.SequenceId = _proxy.GetNextSequenceId();

            return _builder.BuildRequest();
        }
    }
}
