using System;
using System.Collections.Concurrent;

namespace RabbitOM.Streaming.Rtsp
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
            return RtspHeaderAccept.TryParse( headerValue , out RtspHeaderAccept result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderAcceptEncoding( string headerValue )
        {
            return RtspHeaderAcceptEncoding.TryParse( headerValue , out RtspHeaderAcceptEncoding result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderAcceptLanguage( string headerValue )
        {
            return RtspHeaderAcceptLanguage.TryParse( headerValue , out RtspHeaderAcceptLanguage result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderAllow( string headerValue )
        {
            return RtspHeaderAllow.TryParse( headerValue , out RtspHeaderAllow result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderAuthorization( string headerValue )
        {
            return RtspHeaderAuthorization.TryParse( headerValue , out RtspHeaderAuthorization result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderBandwith( string headerValue )
        {
            return RtspHeaderBandwith.TryParse( headerValue , out RtspHeaderBandwith result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderBlockSize( string headerValue )
        {
            return RtspHeaderBlockSize.TryParse( headerValue , out RtspHeaderBlockSize result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderCacheControl( string headerValue )
        {
            return RtspHeaderCacheControl.TryParse( headerValue , out RtspHeaderCacheControl result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderConference( string headerValue )
        {
            return RtspHeaderConference.TryParse( headerValue , out RtspHeaderConference result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderConnection( string headerValue )
        {
            return RtspHeaderConnection.TryParse( headerValue , out RtspHeaderConnection result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentBase( string headerValue )
        {
            return RtspHeaderContentBase.TryParse( headerValue , out RtspHeaderContentBase result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentEncoding( string headerValue )
        {
            return RtspHeaderContentEncoding.TryParse( headerValue , out RtspHeaderContentEncoding result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentLanguage( string headerValue )
        {
            return RtspHeaderContentLanguage.TryParse( headerValue , out RtspHeaderContentLanguage result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentLength( string headerValue )
        {
            return RtspHeaderContentLength.TryParse( headerValue , out RtspHeaderContentLength result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentLocation( string headerValue )
        {
            return RtspHeaderContentLocation.TryParse( headerValue , out RtspHeaderContentLocation result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderContentType( string headerValue )
        {
            return RtspHeaderContentType.TryParse( headerValue , out RtspHeaderContentType result ) ? result : null;
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

            return new RtspHeaderCustom( headerName , headerValue );
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderDate( string headerValue )
        {
            return RtspHeaderDate.TryParse( headerValue , out RtspHeaderDate result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderExpires( string headerValue )
        {
            return RtspHeaderExpires.TryParse( headerValue , out RtspHeaderExpires result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderFrom( string headerValue )
        {
            return RtspHeaderFrom.TryParse( headerValue , out RtspHeaderFrom result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderIfMatch( string headerValue )
        {
            return RtspHeaderIfMatch.TryParse( headerValue , out RtspHeaderIfMatch result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderIfModifiedSince( string headerValue )
        {
            return RtspHeaderIfModifiedSince.TryParse( headerValue , out RtspHeaderIfModifiedSince result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderLastModified( string headerValue )
        {
            return RtspHeaderLastModified.TryParse( headerValue , out RtspHeaderLastModified result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderLocation( string headerValue )
        {
            return RtspHeaderLocation.TryParse( headerValue , out RtspHeaderLocation result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderProxyAuthenticate( string headerValue )
        {
            return RtspHeaderProxyAuthenticate.TryParse( headerValue , out RtspHeaderProxyAuthenticate result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderProxyRequire( string headerValue )
        {
            return RtspHeaderProxyRequire.TryParse( headerValue , out RtspHeaderProxyRequire result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderPublic( string headerValue )
        {
            return RtspHeaderPublic.TryParse( headerValue , out RtspHeaderPublic result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderRange( string headerValue )
        {
            return RtspHeaderRange.TryParse( headerValue , out RtspHeaderRange result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderReferer( string headerValue )
        {
            return RtspHeaderReferer.TryParse( headerValue , out RtspHeaderReferer result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderRequire( string headerValue )
        {
            return RtspHeaderRequire.TryParse( headerValue , out RtspHeaderRequire result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderRetryAfter( string headerValue )
        {
            return RtspHeaderRetryAfter.TryParse( headerValue , out RtspHeaderRetryAfter result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderRtpInfo( string headerValue )
        {
            return RtspHeaderRtpInfo.TryParse( headerValue , out RtspHeaderRtpInfo result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderScale( string headerValue )
        {
            return RtspHeaderScale.TryParse( headerValue , out RtspHeaderScale result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderCSeq( string headerValue )
        {
            return RtspHeaderCSeq.TryParse( headerValue , out RtspHeaderCSeq result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderServer( string headerValue )
        {
            return RtspHeaderServer.TryParse( headerValue , out RtspHeaderServer result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderSession( string headerValue )
        {
            return RtspHeaderSession.TryParse( headerValue , out RtspHeaderSession result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderSpeed( string headerValue )
        {
            return RtspHeaderSpeed.TryParse( headerValue , out RtspHeaderSpeed result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderTransport( string headerValue )
        {
            return RtspHeaderTransport.TryParse( headerValue , out RtspHeaderTransport result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderUserAgent( string headerValue )
        {
            return RtspHeaderUserAgent.TryParse( headerValue , out RtspHeaderUserAgent result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderVary( string headerValue )
        {
            return RtspHeaderVary.TryParse( headerValue , out RtspHeaderVary result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderVia( string headerValue )
        {
            return RtspHeaderVia.TryParse( headerValue , out RtspHeaderVia result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RtspHeader CreateHeaderWWWAuthenticate( string headerValue )
        {
            return RtspHeaderWWWAuthenticate.TryParse( headerValue , out RtspHeaderWWWAuthenticate result ) ? result : null;
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
