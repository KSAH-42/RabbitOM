using System;
using System.Collections.Concurrent;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header factory
    /// </summary>
    public static class RTSPHeaderFactory
    {
        private readonly static ConcurrentDictionary<string,Func<string,RTSPHeader>> s_factories = new ConcurrentDictionary<string, Func<string,RTSPHeader>>( StringComparer.OrdinalIgnoreCase );

        /// <summary>
        /// Constructor
        /// </summary>
        static RTSPHeaderFactory()
        {
            s_factories[RTSPHeaderNames.Accept] = CreateHeaderAccept;
            s_factories[RTSPHeaderNames.AcceptEncoding] = CreateHeaderAcceptEncoding;
            s_factories[RTSPHeaderNames.AcceptLanguage] = CreateHeaderAcceptLanguage;
            s_factories[RTSPHeaderNames.Allow] = CreateHeaderAllow;
            s_factories[RTSPHeaderNames.Authorization] = CreateHeaderAuthorization;
            s_factories[RTSPHeaderNames.Bandwith] = CreateHeaderBandwith;
            s_factories[RTSPHeaderNames.BlockSize] = CreateHeaderBlockSize;
            s_factories[RTSPHeaderNames.CacheControl] = CreateHeaderCacheControl;
            s_factories[RTSPHeaderNames.Conference] = CreateHeaderConference;
            s_factories[RTSPHeaderNames.Connection] = CreateHeaderConnection;
            s_factories[RTSPHeaderNames.ContentBase] = CreateHeaderContentBase;
            s_factories[RTSPHeaderNames.ContentEncoding] = CreateHeaderContentEncoding;
            s_factories[RTSPHeaderNames.ContentLanguage] = CreateHeaderContentLanguage;
            s_factories[RTSPHeaderNames.ContentLength] = CreateHeaderContentLength;
            s_factories[RTSPHeaderNames.ContentLocation] = CreateHeaderContentLocation;
            s_factories[RTSPHeaderNames.ContentType] = CreateHeaderContentType;
            s_factories[RTSPHeaderNames.CSeq] = CreateHeaderCSeq;
            s_factories[RTSPHeaderNames.Date] = CreateHeaderDate;
            s_factories[RTSPHeaderNames.Expires] = CreateHeaderExpires;
            s_factories[RTSPHeaderNames.From] = CreateHeaderFrom;
            s_factories[RTSPHeaderNames.IfMatch] = CreateHeaderIfMatch;
            s_factories[RTSPHeaderNames.IfModifiedSince] = CreateHeaderIfModifiedSince;
            s_factories[RTSPHeaderNames.LastModified] = CreateHeaderLastModified;
            s_factories[RTSPHeaderNames.Location] = CreateHeaderLocation;
            s_factories[RTSPHeaderNames.ProxyAuthenticate] = CreateHeaderProxyAuthenticate;
            s_factories[RTSPHeaderNames.ProxyRequire] = CreateHeaderProxyRequire;
            s_factories[RTSPHeaderNames.Public] = CreateHeaderPublic;
            s_factories[RTSPHeaderNames.Range] = CreateHeaderRange;
            s_factories[RTSPHeaderNames.Referer] = CreateHeaderReferer;
            s_factories[RTSPHeaderNames.Require] = CreateHeaderRequire;
            s_factories[RTSPHeaderNames.RetryAfter] = CreateHeaderRetryAfter;
            s_factories[RTSPHeaderNames.RtpInfo] = CreateHeaderRtpInfo;
            s_factories[RTSPHeaderNames.Scale] = CreateHeaderScale;
            s_factories[RTSPHeaderNames.Server] = CreateHeaderServer;
            s_factories[RTSPHeaderNames.Session] = CreateHeaderSession;
            s_factories[RTSPHeaderNames.Speed] = CreateHeaderSpeed;
            s_factories[RTSPHeaderNames.Transport] = CreateHeaderTransport;
            s_factories[RTSPHeaderNames.UserAgent] = CreateHeaderUserAgent;
            s_factories[RTSPHeaderNames.Vary] = CreateHeaderVary;
            s_factories[RTSPHeaderNames.Via] = CreateHeaderVia;
            s_factories[RTSPHeaderNames.WWWAuthenticate] = CreateHeaderWWWAuthenticate;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderAccept( string headerValue )
        {
            return RTSPHeaderAccept.TryParse( headerValue , out RTSPHeaderAccept result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderAcceptEncoding( string headerValue )
        {
            return RTSPHeaderAcceptEncoding.TryParse( headerValue , out RTSPHeaderAcceptEncoding result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderAcceptLanguage( string headerValue )
        {
            return RTSPHeaderAcceptLanguage.TryParse( headerValue , out RTSPHeaderAcceptLanguage result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderAllow( string headerValue )
        {
            return RTSPHeaderAllow.TryParse( headerValue , out RTSPHeaderAllow result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderAuthorization( string headerValue )
        {
            return RTSPHeaderAuthorization.TryParse( headerValue , out RTSPHeaderAuthorization result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderBandwith( string headerValue )
        {
            return RTSPHeaderBandwith.TryParse( headerValue , out RTSPHeaderBandwith result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderBlockSize( string headerValue )
        {
            return RTSPHeaderBlockSize.TryParse( headerValue , out RTSPHeaderBlockSize result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderCacheControl( string headerValue )
        {
            return RTSPHeaderCacheControl.TryParse( headerValue , out RTSPHeaderCacheControl result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderConference( string headerValue )
        {
            return RTSPHeaderConference.TryParse( headerValue , out RTSPHeaderConference result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderConnection( string headerValue )
        {
            return RTSPHeaderConnection.TryParse( headerValue , out RTSPHeaderConnection result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderContentBase( string headerValue )
        {
            return RTSPHeaderContentBase.TryParse( headerValue , out RTSPHeaderContentBase result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderContentEncoding( string headerValue )
        {
            return RTSPHeaderContentEncoding.TryParse( headerValue , out RTSPHeaderContentEncoding result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderContentLanguage( string headerValue )
        {
            return RTSPHeaderContentLanguage.TryParse( headerValue , out RTSPHeaderContentLanguage result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderContentLength( string headerValue )
        {
            return RTSPHeaderContentLength.TryParse( headerValue , out RTSPHeaderContentLength result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderContentLocation( string headerValue )
        {
            return RTSPHeaderContentLocation.TryParse( headerValue , out RTSPHeaderContentLocation result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderContentType( string headerValue )
        {
            return RTSPHeaderContentType.TryParse( headerValue , out RTSPHeaderContentType result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerName">the header name</param>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderCustom( string headerName , string headerValue )
        {
            if ( string.IsNullOrWhiteSpace( headerValue ) )
            {
                return null;
            }

            return new RTSPHeaderCustom( headerName , headerValue );
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderDate( string headerValue )
        {
            return RTSPHeaderDate.TryParse( headerValue , out RTSPHeaderDate result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderExpires( string headerValue )
        {
            return RTSPHeaderExpires.TryParse( headerValue , out RTSPHeaderExpires result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderFrom( string headerValue )
        {
            return RTSPHeaderFrom.TryParse( headerValue , out RTSPHeaderFrom result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderIfMatch( string headerValue )
        {
            return RTSPHeaderIfMatch.TryParse( headerValue , out RTSPHeaderIfMatch result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderIfModifiedSince( string headerValue )
        {
            return RTSPHeaderIfModifiedSince.TryParse( headerValue , out RTSPHeaderIfModifiedSince result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderLastModified( string headerValue )
        {
            return RTSPHeaderLastModified.TryParse( headerValue , out RTSPHeaderLastModified result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderLocation( string headerValue )
        {
            return RTSPHeaderLocation.TryParse( headerValue , out RTSPHeaderLocation result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderProxyAuthenticate( string headerValue )
        {
            return RTSPHeaderProxyAuthenticate.TryParse( headerValue , out RTSPHeaderProxyAuthenticate result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderProxyRequire( string headerValue )
        {
            return RTSPHeaderProxyRequire.TryParse( headerValue , out RTSPHeaderProxyRequire result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderPublic( string headerValue )
        {
            return RTSPHeaderPublic.TryParse( headerValue , out RTSPHeaderPublic result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderRange( string headerValue )
        {
            return RTSPHeaderRange.TryParse( headerValue , out RTSPHeaderRange result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderReferer( string headerValue )
        {
            return RTSPHeaderReferer.TryParse( headerValue , out RTSPHeaderReferer result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderRequire( string headerValue )
        {
            return RTSPHeaderRequire.TryParse( headerValue , out RTSPHeaderRequire result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderRetryAfter( string headerValue )
        {
            return RTSPHeaderRetryAfter.TryParse( headerValue , out RTSPHeaderRetryAfter result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderRtpInfo( string headerValue )
        {
            return RTSPHeaderRtpInfo.TryParse( headerValue , out RTSPHeaderRtpInfo result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderScale( string headerValue )
        {
            return RTSPHeaderScale.TryParse( headerValue , out RTSPHeaderScale result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderCSeq( string headerValue )
        {
            return RTSPHeaderCSeq.TryParse( headerValue , out RTSPHeaderCSeq result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderServer( string headerValue )
        {
            return RTSPHeaderServer.TryParse( headerValue , out RTSPHeaderServer result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderSession( string headerValue )
        {
            return RTSPHeaderSession.TryParse( headerValue , out RTSPHeaderSession result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderSpeed( string headerValue )
        {
            return RTSPHeaderSpeed.TryParse( headerValue , out RTSPHeaderSpeed result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderTransport( string headerValue )
        {
            return RTSPHeaderTransport.TryParse( headerValue , out RTSPHeaderTransport result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderUserAgent( string headerValue )
        {
            return RTSPHeaderUserAgent.TryParse( headerValue , out RTSPHeaderUserAgent result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderVary( string headerValue )
        {
            return RTSPHeaderVary.TryParse( headerValue , out RTSPHeaderVary result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderVia( string headerValue )
        {
            return RTSPHeaderVia.TryParse( headerValue , out RTSPHeaderVia result ) ? result : null;
        }

        /// <summary>
        /// Create a header
        /// </summary>
        /// <param name="headerValue">the header value</param>
        /// <returns>returns an instance, otherwise null</returns>
        public static RTSPHeader CreateHeaderWWWAuthenticate( string headerValue )
        {
            return RTSPHeaderWWWAuthenticate.TryParse( headerValue , out RTSPHeaderWWWAuthenticate result ) ? result : null;
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
        ///  var header = RTSPHeaderFactory.CreateHeader( "CSeq: 1") 
        /// </code>
        /// </remarks>
        public static RTSPHeader CreateHeader( string entireHeader )
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
        public static RTSPHeader CreateHeader( string headerName , string headerValue )
        {
            if ( string.IsNullOrWhiteSpace( headerValue ) || string.IsNullOrWhiteSpace( headerValue ) )
            {
                return null;
            }

            var header = headerName.Trim();

            if ( s_factories.TryGetValue( header , out Func<string , RTSPHeader> factory ) && factory != null )
            {
                return factory.Invoke( headerValue );
            }

            return CreateHeaderCustom( header , headerValue );
        }
    }
}
