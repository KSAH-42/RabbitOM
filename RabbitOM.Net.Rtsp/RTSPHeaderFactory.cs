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
            s_factories[RTSPHeaderNames.Accept] = new Func<string , RTSPHeader>( value => CreateHeaderAccept( value ) );
            s_factories[RTSPHeaderNames.AcceptEncoding] = new Func<string , RTSPHeader>( value => CreateHeaderAcceptEncoding( value ) );
            s_factories[RTSPHeaderNames.AcceptLanguage] = new Func<string , RTSPHeader>( value => CreateHeaderAcceptLanguage( value ) );
            s_factories[RTSPHeaderNames.Allow] = new Func<string , RTSPHeader>( value => CreateHeaderAllow( value ) );
            s_factories[RTSPHeaderNames.Authorization] = new Func<string , RTSPHeader>( value => CreateHeaderAuthorization( value ) );
            s_factories[RTSPHeaderNames.Bandwith] = new Func<string , RTSPHeader>( value => CreateHeaderBandwith( value ) );
            s_factories[RTSPHeaderNames.BlockSize] = new Func<string , RTSPHeader>( value => CreateHeaderBlockSize( value ) );
            s_factories[RTSPHeaderNames.CacheControl] = new Func<string , RTSPHeader>( value => CreateHeaderCacheControl( value ) );
            s_factories[RTSPHeaderNames.Conference] = new Func<string , RTSPHeader>( value => CreateHeaderConference( value ) );
            s_factories[RTSPHeaderNames.Connection] = new Func<string , RTSPHeader>( value => CreateHeaderConnection( value ) );
            s_factories[RTSPHeaderNames.ContentBase] = new Func<string , RTSPHeader>( value => CreateHeaderContentBase( value ) );
            s_factories[RTSPHeaderNames.ContentEncoding] = new Func<string , RTSPHeader>( value => CreateHeaderContentEncoding( value ) );
            s_factories[RTSPHeaderNames.ContentLanguage] = new Func<string , RTSPHeader>( value => CreateHeaderContentLanguage( value ) );
            s_factories[RTSPHeaderNames.ContentLength] = new Func<string , RTSPHeader>( value => CreateHeaderContentLength( value ) );
            s_factories[RTSPHeaderNames.ContentLocation] = new Func<string , RTSPHeader>( value => CreateHeaderContentLocation( value ) );
            s_factories[RTSPHeaderNames.ContentType] = new Func<string , RTSPHeader>( value => CreateHeaderContentType( value ) );
            s_factories[RTSPHeaderNames.CSeq] = new Func<string , RTSPHeader>( value => CreateHeaderCSeq( value ) );
            s_factories[RTSPHeaderNames.Date] = new Func<string , RTSPHeader>( value => CreateHeaderDate( value ) );
            s_factories[RTSPHeaderNames.Expires] = new Func<string , RTSPHeader>( value => CreateHeaderExpires( value ) );
            s_factories[RTSPHeaderNames.From] = new Func<string , RTSPHeader>( value => CreateHeaderFrom( value ) );
            s_factories[RTSPHeaderNames.IfMatch] = new Func<string , RTSPHeader>( value => CreateHeaderIfMatch( value ) );
            s_factories[RTSPHeaderNames.IfModifiedSince] = new Func<string , RTSPHeader>( value => CreateHeaderIfModifiedSince( value ) );
            s_factories[RTSPHeaderNames.LastModified] = new Func<string , RTSPHeader>( value => CreateHeaderLastModified( value ) );
            s_factories[RTSPHeaderNames.Location] = new Func<string , RTSPHeader>( value => CreateHeaderLocation( value ) );
            s_factories[RTSPHeaderNames.ProxyAuthenticate] = new Func<string , RTSPHeader>( value => CreateHeaderProxyAuthenticate( value ) );
            s_factories[RTSPHeaderNames.ProxyRequire] = new Func<string , RTSPHeader>( value => CreateHeaderProxyRequire( value ) );
            s_factories[RTSPHeaderNames.Public] = new Func<string , RTSPHeader>( value => CreateHeaderPublic( value ) );
            s_factories[RTSPHeaderNames.Range] = new Func<string , RTSPHeader>( value => CreateHeaderRange( value ) );
            s_factories[RTSPHeaderNames.Referer] = new Func<string , RTSPHeader>( value => CreateHeaderReferer( value ) );
            s_factories[RTSPHeaderNames.Require] = new Func<string , RTSPHeader>( value => CreateHeaderRequire( value ) );
            s_factories[RTSPHeaderNames.RetryAfter] = new Func<string , RTSPHeader>( value => CreateHeaderRetryAfter( value ) );
            s_factories[RTSPHeaderNames.RtpInfo] = new Func<string , RTSPHeader>( value => CreateHeaderRtpInfo( value ) );
            s_factories[RTSPHeaderNames.Scale] = new Func<string , RTSPHeader>( value => CreateHeaderScale( value ) );
            s_factories[RTSPHeaderNames.Server] = new Func<string , RTSPHeader>( value => CreateHeaderServer( value ) );
            s_factories[RTSPHeaderNames.Session] = new Func<string , RTSPHeader>( value => CreateHeaderSession( value ) );
            s_factories[RTSPHeaderNames.Speed] = new Func<string , RTSPHeader>( value => CreateHeaderSpeed( value ) );
            s_factories[RTSPHeaderNames.Transport] = new Func<string , RTSPHeader>( value => CreateHeaderTransport( value ) );
            s_factories[RTSPHeaderNames.UserAgent] = new Func<string , RTSPHeader>( value => CreateHeaderUserAgent( value ) );
            s_factories[RTSPHeaderNames.Vary] = new Func<string , RTSPHeader>( value => CreateHeaderVary( value ) );
            s_factories[RTSPHeaderNames.Via] = new Func<string , RTSPHeader>( value => CreateHeaderVia( value ) );
            s_factories[RTSPHeaderNames.WWWAuthenticate] = new Func<string , RTSPHeader>( value => CreateHeaderWWWAuthenticate( value ) );
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
            if ( string.IsNullOrWhiteSpace( entireHeader ) || string.IsNullOrWhiteSpace( entireHeader ) || string.IsNullOrWhiteSpace( headerName ) )
            {
                return false;
            }

            var tokens = entireHeader.Split( new char[] { ':' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length <= 1 )
            {
                return false;
            }

            var header = tokens[ 0 ].Trim();

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
            if ( string.IsNullOrWhiteSpace( entireHeader ) || string.IsNullOrWhiteSpace( entireHeader ) )
            {
                return null;
            }

            var tokens = entireHeader.Split( new char[] { ':' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length <= 1 )
            {
                return null;
            }

            var header = tokens[ 0 ].Trim();

            if ( s_factories.TryGetValue( header , out Func<string , RTSPHeader> factory ) && factory != null )
            {
                return factory.Invoke( tokens[1] );
            }

            return CreateHeaderCustom( header , tokens[1] );
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
