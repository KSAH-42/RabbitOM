using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message request
    /// </summary>
    public sealed class RTSPRequest : RTSPMessage
    {
        private readonly RTSPMethod                 _method   = RTSPMethod.UnDefined;

        private readonly string                         _uri      = string.Empty;

        private readonly RTSPHeaderCollection                 _headers  = null;

        private readonly RTSPMessageBody                _body     = null;

        private readonly RTSPMessageVersion             _version  = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method">the status code</param>
        /// <param name="uri">the uri</param>
        public RTSPRequest( RTSPMethod method , string uri )
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
        public RTSPRequest( RTSPMethod method , string uri , RTSPMessageVersion version )
        {
            if ( version == null )
            {
                throw new ArgumentNullException( nameof( version ) );
            }

            _method  = method;
            _uri     = RTSPDataConverter.Trim( uri );
            _headers = new RTSPHeaderCollection();
            _body    = new RTSPMessageBody();
            _version = version;
        }





        /// <summary>
        /// Gets the method
        /// </summary>
        public RTSPMethod Method
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
        public override RTSPHeaderCollection Headers
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
        public override bool TryValidate()
        {
            if ( _method == RTSPMethod.UnDefined )
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

            return _version.TryValidate();
        }


        /// <summary>
        /// Create an undefined request
        /// </summary>
        /// <returns>returns an instance</returns>
        public static RTSPRequest CreateUnDefinedRequest()
        {
            return new RTSPRequest( RTSPMethod.UnDefined , string.Empty );
        }
    }
}
