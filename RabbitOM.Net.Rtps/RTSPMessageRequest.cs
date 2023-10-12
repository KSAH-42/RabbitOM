using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a message request
    /// </summary>
    public sealed class RTSPMessageRequest : RTSPMessage
    {
        private readonly RTSPMethodType                 _method   = RTSPMethodType.UnDefined;

        private readonly string                         _uri      = string.Empty;

        private readonly RTSPHeaderList                 _headers  = null;

        private readonly RTSPMessageBody                _body     = null;

        private readonly RTSPMessageVersion             _version  = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method">the status code</param>
        /// <param name="uri">the uri</param>
        public RTSPMessageRequest( RTSPMethodType method , string uri )
            : this( method , uri , RTSPMessageVersion.Version_1_0 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method">the status code</param>
        /// <param name="uri">the uri</param>
        /// <param name="version">the version</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPMessageRequest( RTSPMethodType method , string uri , RTSPMessageVersion version )
        {
            if ( version == null )
            {
                throw new ArgumentNullException( nameof( version ) );
            }

            _method = method;
            _uri = RTSPDataFilter.Trim( uri );
            _headers = new RTSPHeaderList();
            _body = new RTSPMessageBody();
            _version = version;
        }





        /// <summary>
        /// Gets the method
        /// </summary>
        public RTSPMethodType Method
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
        public override RTSPHeaderList Headers
        {
            get => _headers;
        }

        /// <summary>
        /// Gets the body
        /// </summary>
        public override RTSPMessageBody Body
        {
            get => _body;
        }

        /// <summary>
        /// Gets the version
        /// </summary>
        public override RTSPMessageVersion Version
        {
            get => _version;
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool Validate()
        {
            if ( _method == RTSPMethodType.UnDefined )
            {
                return false;
            }

            if ( !System.Uri.TryCreate( _uri , UriKind.Absolute , out System.Uri uri ) || string.Compare( uri.Scheme , "rtsp" ) != 0 )
            {
                return false;
            }

            if ( !_headers.ContainsKey( RTSPHeaderNames.CSeq ) )
            {
                return false;
            }

            return _version.Validate();
        }


        /// <summary>
        /// Create an undefined request
        /// </summary>
        /// <returns>returns an instance</returns>
        public static RTSPMessageRequest CreateUnDefinedRequest()
        {
            return new RTSPMessageRequest( RTSPMethodType.UnDefined , string.Empty );
        }
    }
}
