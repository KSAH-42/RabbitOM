using System;
using System.Text;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a response deserializer
    /// </summary>
    internal static class RTSPMessageSerializer
    {
        /// <summary>
        /// Serialize a request
        /// </summary>
        /// <param name="request">the request</param>
        /// <returns>returns a string</returns>
        /// <remarks>
        ///   <para>Please, note that this method doesn't make any validations before to perform a serialization</para>
        /// </remarks>
        public static string Serialize( RTSPMessageRequest request )
        {
            if ( request == null || request.Method == RTSPMethod.UnDefined )
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.Append( RTSPDataConverter.ConvertToString( request.Method ) );
            builder.Append( " " );
            builder.Append( request.Uri );
            builder.Append( " " );
            builder.AppendFormat( "RTSP/{0}.{1}" , request.Version.MajorNumber , request.Version.MinorNumber );
            builder.AppendLine();

            foreach ( var header in request.Headers )
            {
                if ( !header.TryValidate() )
                {
                    continue;
                }

                var headerValue = header.ToString();

                if ( string.IsNullOrWhiteSpace( headerValue ) )
                {
                    continue;
                }

                builder.AppendLine( $"{header.Name}: {headerValue}" );
            }

            builder.AppendLine();

            if ( request.Body.HasValue )
            {
                builder.Append( request.Body.Value );
            }

            return builder.ToString();
        }

        /// <summary>
        /// Deserialize a response
        /// </summary>
        /// <param name="input">the input value</param>
        /// <returns>returns a response, otherwise null</returns>
        /// <remarks>
        ///   <para>Please, note that this method doesn't make any validations, this class only deserialize an object</para>
        /// </remarks>
        public static RTSPMessageResponse Deserialize( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return null;
            }

            using ( var reader = new RTSPMessageResponseReader( input ) )
            {
                if ( reader.ReadLineStatus() )
                {
                    reader.ReadHeaders();
                    reader.ReadLine();
                    reader.ReadBody();

                    var response = new RTSPMessageResponse( new RTSPMessageStatus( reader.GetStatusCode() , reader.GetStatusReason() ) , new RTSPMessageVersion( reader.GetMajorVersion() , reader.GetMinorVersion() ) );

                    foreach ( var header in reader.GetHeaders() )
                    {
                        response.Headers.TryAdd( RTSPHeaderFactory.CreateHeader( header.Key , header.Value ) );
                    }

                    response.Body.Value = reader.GetBody();

                    return response;
                }

                return null;
            }
        }
    }
}
