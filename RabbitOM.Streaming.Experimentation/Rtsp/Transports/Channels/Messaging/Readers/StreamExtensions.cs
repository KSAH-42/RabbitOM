using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    internal static class StreamExtensions
    {
        public static string ReadLine( this Stream stream )
        {
            Debug.Assert( stream != null );

            var sb = new StringBuilder();
            
            while ( true )
            {
                var byteRead = stream.ReadByte();

                if ( byteRead < 0 )
                {
                    return null;
                }

                if ( byteRead == 0 || byteRead == '\r' )
                {
                    break;
                }

                if ( byteRead != '\n' )
                {
                    sb.Append( (char) byteRead );
                }
            }

            return sb.ToString();
        }
    }
}
