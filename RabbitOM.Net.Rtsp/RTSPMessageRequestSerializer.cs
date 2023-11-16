using System.Text;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a request serializer
    /// </summary>
    public static class RTSPMessageRequestSerializer
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

            builder.Append(RTSPDataConverter.ConvertToString( request.Method ) );
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
    }
}
