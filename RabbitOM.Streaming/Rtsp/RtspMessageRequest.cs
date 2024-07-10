using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message request
    /// </summary>
    public sealed class RtspMessageRequest : RtspMessage
    {
        private readonly RtspMethod             _method   = RtspMethod.UnDefined;

        private readonly string                 _uri      = string.Empty;

        private readonly RtspHeaderCollection   _headers  = null;

        private readonly RtspMessageBody        _body     = null;

        private readonly RtspMessageVersion     _version  = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method">the status code</param>
        /// <param name="uri">the uri</param>
        public RtspMessageRequest( RtspMethod method , string uri )
            : this( method , uri , RtspMessageVersion.Version_1_0 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method">the status code</param>
        /// <param name="uri">the uri</param>
        /// <param name="version">the version</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspMessageRequest( RtspMethod method , string uri , RtspMessageVersion version )
        {
            if ( version == null )
            {
                throw new ArgumentNullException( nameof( version ) );
            }

            _method  = method;
            _uri     = RtspDataConverter.Trim( uri );
            _headers = new RtspHeaderCollection();
            _body    = new RtspMessageBody();
            _version = version;
        }





        /// <summary>
        /// Gets the method
        /// </summary>
        public RtspMethod Method
        {
            get => _method;
        }

        /// <summary>
        /// Gets the uri
        /// </summary>
        public string Uri
        {
            get => _uri;
        }

        /// <summary>
        /// Gets the headers
        /// </summary>
        public override RtspHeaderCollection Headers
        {
            get => _headers;
        }

        /// <summary>
        /// Gets the body
        /// </summary>
        public override RtspMessageBody Body
        {
            get => _body;
        }

        /// <summary>
        /// Gets the version
        /// </summary>
        public override RtspMessageVersion Version
        {
            get => _version;
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( _method == RtspMethod.UnDefined )
            {
                return false;
            }

            if ( !System.Uri.TryCreate( _uri , UriKind.Absolute , out System.Uri uri ) || string.Compare( uri.Scheme , "Rtsp" ) != 0 )
            {
                return false;
            }

            if ( !_headers.ContainsKey( RtspHeaderNames.CSeq ) )
            {
                return false;
            }

            return _version.TryValidate();
        }


        /// <summary>
        /// Create an undefined request
        /// </summary>
        /// <returns>returns an instance</returns>
        internal static RtspMessageRequest CreateUnDefinedRequest()
        {
            return new RtspMessageRequest( RtspMethod.UnDefined , string.Empty );
        }
    }
}
