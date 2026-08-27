using System;
using System.Collections.Concurrent;

namespace RabbitOM.Net.Rtsp.Headers
{
    /// <summary>
    /// Represent a message header factory
    /// </summary>
    public static class RtspHeaderFactory
    {
        private readonly static ConcurrentDictionary<string,Func<string,RtspHeader>> s_factories = new ConcurrentDictionary<string, Func<string,RtspHeader>>( StringComparer.OrdinalIgnoreCase );

        /// <summary>
        /// Constructor
        /// </summary>
        static RtspHeaderFactory()
        {
            s_factories[RtspHeaderNames.Accept] = CreateHeaderAccept;
            s_factories[RtspHeaderNames.AcceptEncoding] = CreateHeaderAcceptEncoding;
            s_factories[RtspHeaderNames.AcceptLanguage] = CreateHeaderAcceptLanguage;
            s_factories[RtspHeaderNames.Allow] = CreateHeaderAllow;
            s_factories[RtspHeaderNames.Authorization] = CreateHeaderAuthorization;
            s_factories[RtspHeaderNames.Bandwith] = CreateHeaderBandwith;
            s_factories[RtspHeaderNames.BlockSize] = CreateHeaderBlockSize;
            s_factories[RtspHeaderNames.CacheControl] = CreateHeaderCacheControl;
            s_factories[RtspHeaderNames.Conference] = CreateHeaderConference;
            s_factories[RtspHeaderNames.Connection] = CreateHeaderConnection;
            s_factories[RtspHeaderNames.ContentBase] = CreateHeaderContentBase;
            s_factories[RtspHeaderNames.ContentEncoding] = CreateHeaderContentEncoding;
            s_factories[RtspHeaderNames.ContentLanguage] = CreateHeaderContentLanguage;
            s_factories[RtspHeaderNames.ContentLength] = CreateHeaderContentLength;
            s_factories[RtspHeaderNames.ContentLocation] = CreateHeaderContentLocation;
            s_factories[RtspHeaderNames.ContentType] = CreateHeaderContentType;
            s_factories[RtspHeaderNames.CSeq] = CreateHeaderCSeq;
            s_factories[RtspHeaderNames.Date] = CreateHeaderDate;
            s_factories[RtspHeaderNames.Expires] = CreateHeaderExpires;
            s_factories[RtspHeaderNames.From] = CreateHeaderFrom;
            s_factories[RtspHeaderNames.IfMatch] = CreateHeaderIfMatch;
            s_factories[RtspHeaderNames.IfModifiedSince] = CreateHeaderIfModifiedSince;
            s_factories[RtspHeaderNames.LastModified] = CreateHeaderLastModified;
            s_factories[RtspHeaderNames.Location] = CreateHeaderLocation;
            s_factories[RtspHeaderNames.ProxyAuthenticate] = CreateHeaderProxyAuthenticate;
            s_factories[RtspHeaderNames.ProxyRequire] = CreateHeaderProxyRequire;
            s_factories[RtspHeaderNames.Public] = CreateHeaderPublic;
            s_factories[RtspHeaderNames.Range] = CreateHeaderRange;
            s_factories[RtspHeaderNames.Referer] = CreateHeaderReferer;
            s_factories[RtspHeaderNames.Require] = CreateHeaderRequire;
            s_factories[RtspHeaderNames.RetryAfter] = CreateHeaderRetryAfter;
            s_factories[RtspHeaderNames.RtpInfo] = CreateHeaderRtpInfo;
            s_factories[RtspHeaderNames.Scale] = CreateHeaderScale;
            s_factories[RtspHeaderNames.Server] = CreateHeaderServer;
            s_factories[RtspHeaderNames.Session] = CreateHeaderSession;
            s_factories[RtspHeaderNames.Speed] = CreateHeaderSpeed;
            s_factories[RtspHeaderNames.Transport] = CreateHeaderTransport;
            s_factories[RtspHeaderNames.UserAgent] = CreateHeaderUserAgent;
            s_factories[RtspHeaderNames.Vary] = CreateHeaderVary;
            s_factories[RtspHeaderNames.Via] = CreateHeaderVia;
            s_factories[RtspHeaderNames.WWWAuthenticate] = CreateHeaderWWWAuthenticate;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderAccept( string headerValue )
        {
            return AcceptRtspHeader.TryParse( headerValue , out AcceptRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderAcceptEncoding( string headerValue )
        {
            return AcceptEncodingRtspHeader.TryParse( headerValue , out AcceptEncodingRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderAcceptLanguage( string headerValue )
        {
            return AcceptLanguageRtspHeader.TryParse( headerValue , out AcceptLanguageRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderAllow( string headerValue )
        {
            return AllowRtspHeader.TryParse( headerValue , out AllowRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderAuthorization( string headerValue )
        {
            return AuthorizationRtspHeader.TryParse( headerValue , out AuthorizationRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderBandwith( string headerValue )
        {
            return BandwithRtspHeader.TryParse( headerValue , out BandwithRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderBlockSize( string headerValue )
        {
            return BlockSizeRtspHeader.TryParse( headerValue , out BlockSizeRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderCacheControl( string headerValue )
        {
            return CacheControlRtspHeader.TryParse( headerValue , out CacheControlRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderConference( string headerValue )
        {
            return ConferenceRtspHeader.TryParse( headerValue , out ConferenceRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderConnection( string headerValue )
        {
            return ConnectionRtspHeader.TryParse( headerValue , out ConnectionRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentBase( string headerValue )
        {
            return ContentBaseRtspHeader.TryParse( headerValue , out ContentBaseRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentEncoding( string headerValue )
        {
            return ContentEncodingRtspHeader.TryParse( headerValue , out ContentEncodingRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentLanguage( string headerValue )
        {
            return ContentLanguageRtspHeader.TryParse( headerValue , out ContentLanguageRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentLength( string headerValue )
        {
            return ContentLengthRtspHeader.TryParse( headerValue , out ContentLengthRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentLocation( string headerValue )
        {
            return ContentLocationRtspHeader.TryParse( headerValue , out ContentLocationRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentType( string headerValue )
        {
            return ContentTypeRtspHeader.TryParse( headerValue , out ContentTypeRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerName">the header name</param>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderCustom( string headerName , string headerValue )
        {
            if ( string.IsNullOrWhiteSpace( headerValue ) )
            {
                return null;
            }

            return new CustomRtspHeader( headerName , headerValue );
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderDate( string headerValue )
        {
            return DateRtspHeader.TryParse( headerValue , out DateRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderExpires( string headerValue )
        {
            return ExpiresRtspHeader.TryParse( headerValue , out ExpiresRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderFrom( string headerValue )
        {
            return FromRtspHeader.TryParse( headerValue , out FromRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderIfMatch( string headerValue )
        {
            return IfMatchRtspHeader.TryParse( headerValue , out IfMatchRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderIfModifiedSince( string headerValue )
        {
            return IfModifiedSinceRtspHeader.TryParse( headerValue , out IfModifiedSinceRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderLastModified( string headerValue )
        {
            return LastModifiedRtspHeader.TryParse( headerValue , out LastModifiedRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderLocation( string headerValue )
        {
            return LocationRtspHeader.TryParse( headerValue , out LocationRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderProxyAuthenticate( string headerValue )
        {
            return ProxyAuthenticateRtspHeader.TryParse( headerValue , out ProxyAuthenticateRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderProxyRequire( string headerValue )
        {
            return ProxyRequireRtspHeader.TryParse( headerValue , out ProxyRequireRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderPublic( string headerValue )
        {
            return PublicRtspHeader.TryParse( headerValue , out PublicRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderRange( string headerValue )
        {
            return RangeRtspHeader.TryParse( headerValue , out RangeRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderReferer( string headerValue )
        {
            return RefererRtspHeader.TryParse( headerValue , out RefererRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderRequire( string headerValue )
        {
            return RequireRtspHeader.TryParse( headerValue , out RequireRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderRetryAfter( string headerValue )
        {
            return RetryAfterRtspHeader.TryParse( headerValue , out RetryAfterRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderRtpInfo( string headerValue )
        {
            return RtpInfoRtspHeader.TryParse( headerValue , out RtpInfoRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderScale( string headerValue )
        {
            return ScaleRtspHeader.TryParse( headerValue , out ScaleRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderCSeq( string headerValue )
        {
            return CSeqRtspHeader.TryParse( headerValue , out CSeqRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderServer( string headerValue )
        {
            return ServerRtspHeader.TryParse( headerValue , out ServerRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderSession( string headerValue )
        {
            return SessionRtspHeader.TryParse( headerValue , out SessionRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderSpeed( string headerValue )
        {
            return SpeedRtspHeader.TryParse( headerValue , out SpeedRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderTransport( string headerValue )
        {
            return TransportRtspHeader.TryParse( headerValue , out TransportRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderUserAgent( string headerValue )
        {
            return UserAgentRtspHeader.TryParse( headerValue , out UserAgentRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderVary( string headerValue )
        {
            return VaryRtspHeader.TryParse( headerValue , out VaryRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderVia( string headerValue )
        {
            return ViaRtspHeader.TryParse( headerValue , out ViaRtspHeader result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderWWWAuthenticate( string headerValue )
        {
            return WWWAuthenticateRtspHeader.TryParse( headerValue , out WWWAuthenticateRtspHeader result ) ? result : null;
        }



        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="entireHeader">the full header included it's value</param>
        /// <param name="headerName">the header type name</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool CanCreateHeader( string entireHeader , string headerName )
        {
            if ( string.IsNullOrWhiteSpace( entireHeader ) || string.IsNullOrWhiteSpace( headerName ) )
            {
                return false;
            }

            var tokens = entireHeader.Split( new char[] { ':' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens.Length <= 1 )
            {
                return false;
            }

            var header = tokens[ 0 ]?.Trim() ?? string.Empty;

            return string.Compare( header , headerName.Trim() , true ) == 0;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="entireHeader">the full header: this value must contains the header name and the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        /// <remarks>
        /// <code>
        ///  var header = RtspHeaderFactory.CreateHeader( "CSeq: 1") 
        /// </code>
        /// </remarks>
        public static RtspHeader CreateHeader( string entireHeader )
        {
            if ( string.IsNullOrWhiteSpace( entireHeader ) )
            {
                return null;
            }

            var tokens = entireHeader.Split( new char[] { ':' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens.Length <= 1 )
            {
                return null;
            }

            return CreateHeader( tokens[0] , tokens[1] );
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerName">the header name</param>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeader( string headerName , string headerValue )
        {
            if ( string.IsNullOrWhiteSpace( headerName ) || string.IsNullOrWhiteSpace( headerValue ) )
            {
                return null;
            }

            var header = headerName.Trim();

            if ( s_factories.TryGetValue( header , out Func<string , RtspHeader> factory ) )
            {
                return factory.Invoke( headerValue );
            }

            return CreateHeaderCustom( header , headerValue );
        }
    }
}
