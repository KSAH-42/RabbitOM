using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an uri class
    /// </summary>
    public sealed class RtspUri
    {
        /// <summary>
        /// Represent the default scheme
        /// </summary>
        public const string     DefaultScheme = "Rtsp";

        /// <summary>
        /// Represent the default port
        /// </summary>
        public const int        DefaultPort   = 554;





        private string          _userName     = string.Empty;

        private string          _password     = string.Empty;

        private string          _host         = string.Empty;

        private int             _port         = DefaultPort;

        private string          _path         = string.Empty;

        private string          _query        = string.Empty;





        /// <summary>
        /// Constructor
        /// </summary>
        public RtspUri()
        {
            RtspUri.TryRegisterUriScheme();
        }





        /// <summary>
        /// Gets / Sets the user name
        /// </summary>
        public string UserName
        {
            get => _userName;
            set => _userName = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the password
        /// </summary>
        public string Password
        {
            get => _password;
            set => _password = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the host
        /// </summary>
        public string Host
        {
            get => _host;
            set => _host = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the port
        /// </summary>
        public int Port
        {
            get => _port;
            set => _port = RtspDataConverter.Adapt( value , 1 , ushort.MaxValue );
        }

        /// <summary>
        /// Gets / Sets the path
        /// </summary>
        public string Path
        {
            get => _path;
            set => _path = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the query
        /// </summary>
        public string Query
        {
            get => _query;
            set => _query = RtspDataConverter.Trim( value ).Replace( "?" , "" );
        }





        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryValidate()
        {
            if ( string.IsNullOrWhiteSpace( _host ) || _port <= 0 )
            {
                return false;
            }

            if ( !string.IsNullOrWhiteSpace( _password ) && string.IsNullOrWhiteSpace( _userName ) )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Format 
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return ToString( false );
        }

        /// <summary>
        /// Format 
        /// </summary>
        /// <param name="ignoreCredentials">set true to ignore credentials</param>
        /// <returns>returns a string</returns>
        public string ToString( bool ignoreCredentials )
        {
            try
            {
                var builder = new UriBuilder()
                {
                    Scheme = DefaultScheme ,
                    Host   = _host ,
                    Path   = _path ,
                    Query  = _query
                };

                if ( _port != DefaultPort )
                {
                    builder.Port = _port;
                }

                if ( ! ignoreCredentials && ! string.IsNullOrWhiteSpace( _userName ) )
                {
                    builder.UserName = _userName;
                    builder.Password = _password;
                }

                return builder.ToString();
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return string.Empty;
        }

        /// <summary>
        /// Reset to the default
        /// </summary>
        public void ToDefault()
        {
            _userName = string.Empty;
            _password = string.Empty;
            _host = string.Empty;
            _path = string.Empty;
            _query = string.Empty;
            _port = DefaultPort;
        }

        /// <summary>
        /// Remove the credentials
        /// </summary>
        public void ClearCredentials()
        {
            _userName = string.Empty;
            _password = string.Empty;
        }

        /// <summary>
        /// Perform a copy from a different instance
        /// </summary>
        /// <param name="uri">the uri</param>
        public void CopyFrom( RtspUri uri )
        {
            if ( uri == null || object.ReferenceEquals( uri , this ) )
            {
                return;
            }

            _userName = uri._userName;
            _password = uri._password;
            _host = uri._host;
            _port = uri._port;
            _path = uri._path;
            _query = uri._query;
        }

        /// <summary>
        /// Combine the uri
        /// </summary>
        /// <param name="sdpControlUri">the control uri from the sdp document</param>
        /// <returns>returns a string</returns>
        public string ToControlUri( string sdpControlUri )
        {
            if ( string.IsNullOrWhiteSpace( sdpControlUri ) )
            {
                return ToString();
            }

            var trackUri = sdpControlUri.Replace( " " , "" );

            if ( Uri.IsWellFormedUriString( trackUri , UriKind.RelativeOrAbsolute ) )
            {
                if ( trackUri.StartsWith( DefaultScheme , StringComparison.OrdinalIgnoreCase ) )
                {
                    return trackUri;
                }
            }

            while ( trackUri.StartsWith( "/" ) )
            {
                trackUri = trackUri.Remove( 0 , 1 );
            }

            string rtspUri = ToString();

            while ( rtspUri.EndsWith( "/" ) )
            {
                rtspUri = rtspUri.Remove( rtspUri.Length - 1 , 1 );
            }

            string finalRtspUri = rtspUri + "/" + trackUri;

            if ( Uri.IsWellFormedUriString( finalRtspUri , UriKind.RelativeOrAbsolute ) )
            {
                return finalRtspUri;
            }

            return string.Empty;
        }





        /// <summary>
        /// Check if it a valid Rtsp uri
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <returns>returns true for a success otherwise false.</returns>
        public static bool IsWellFormed( string uri )
        {
            if ( string.IsNullOrWhiteSpace( uri ) )
            {
                return false;
            }

            var rtspUri = uri.Trim();

            if ( Uri.IsWellFormedUriString( rtspUri , UriKind.Absolute ) )
            {
                return rtspUri.StartsWith( DefaultScheme );
            }

            return false;
        }


        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns an instance</returns>
        public static RtspUri Create( string value )
        {
            return TryParse( value , out RtspUri result ) ? result : new RtspUri();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns instance of the uri</returns>
        /// <exception cref="FormatException"/>
        /// <exception cref="ArgumentNullException"/>
        public static RtspUri Parse( string value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return TryParse( value , out RtspUri uri ) ? uri : throw new FormatException();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="result">the uri</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( string value , out RtspUri result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            try
            {
                result = new RtspUri();  // call first to invoke RegisterUriScheme

                var builder = new UriBuilder( value );

                if ( string.Compare( builder.Scheme , DefaultScheme , true ) != 0 )
                {
                    result = null;
                    return false;
                }

                result.UserName = builder.UserName;
                result.Password = builder.Password;
                result.Host = builder.Host;
                result.Port = builder.Port;
                result.Path = builder.Path;
                result.Query = builder.Query;

                return true;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return false;
        }

        /// <summary>
        /// Regiter the Rtsp uri scheme
        /// </summary>
        /// <returns>returns true for a success, othwise false</returns>
        private static bool RegisterUriScheme()
        {
            if ( UriParser.IsKnownScheme( DefaultScheme ) )
            {
                return true;
            }

            UriParser.Register( new HttpStyleUriParser() , DefaultScheme , DefaultPort );

            return true;
        }

        /// <summary>
        /// Try to regiter the Rtsp uri scheme
        /// </summary>
        /// <returns>returns true for a success, othwise false</returns>
        private static bool TryRegisterUriScheme()
        {
            try
            {
                return RegisterUriScheme();
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message );
            }

            return false;
        }
    }
}
