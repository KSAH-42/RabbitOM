// here we don't use string.split at lower level
// the perf result show signatificative improvement
using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspRequestLine
    {
        public string Method { get; set; }

        public string Uri { get; set; }

        public string Protocol { get; set; }

        public string Version { get; set; }




        public static bool TryParse( string input , out RtspRequestLine result )
        {
            result = null;

            if ( string.IsNullOrEmpty( input ) )
            {
                return false;
            }

            // DESCRIBE rtsp://1.1.1.1/predestination RTSP/1.0

            var requestLine = new RtspRequestLine();
            var builder = new StringBuilder();
            var i = -1;

            while ( ++ i < input.Length )
            {
                if ( input[ i ] == ' ' )
                {
                    continue;
                }

                if ( requestLine.Method == null )
                {
                    while ( i < input.Length && input[i] != ' ' )
                    {
                        builder.Append( input[i++] );
                    }

                    requestLine.Method = builder.ToString();
                }
                else if ( requestLine.Uri == null )
                {
                    while ( i < input.Length && input[i] != ' ' )
                    {
                        builder.Append( input[i++] );
                    }

                    requestLine.Uri = builder.ToString();
                }
                else if ( requestLine.Protocol == null )
                {
                    while ( i < input.Length && input[i] != '/' )
                    {
                        var character = input[i++];

                        if ( character != ' ' )
                        {
                            builder.Append( character );
                        }
                    }

                    requestLine.Protocol = builder.ToString();
                }
                else if ( requestLine.Version == null )
                {
                    while ( i < input.Length )
                    {
                        if ( input[i] != ' ' )
                        {
                            builder.Append( input[i] );
                        }

                        i++;
                    }

                    requestLine.Version = builder.ToString();
                }

                builder.Clear();
            }

            result = string.IsNullOrEmpty( requestLine.Method )
                  || string.IsNullOrEmpty( requestLine.Uri )
                  || string.IsNullOrEmpty( requestLine.Protocol )
                  || string.IsNullOrEmpty( requestLine.Version ) ? null : requestLine;

            return result != null;
        }




        public override string ToString()
        {
            // we are at the low level, do not add null empty checks
            return $"{Method} {Uri} {Protocol}/{Version}"; 
        }
    }
}
