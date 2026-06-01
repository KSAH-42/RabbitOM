// here we don't use string.split at lower level
// the perf result show signatificative improvement
using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspStatusLine
    {
        public string Protocol { get; set; }

        public string Version { get; set; }

        public string Code { get; set; }

        public string Reason { get; set; }



        public static bool TryParse( string input , out RtspStatusLine result )
        {
            result = null;

            if ( string.IsNullOrEmpty( input ) )
            {
                return false;
            }

            // RTSP/1.0 555 Doom-Patrol Dark-Series 

            var statusLine = new RtspStatusLine();
            var builder = new StringBuilder();
            var i = -1;

            while ( ++ i < input.Length )
            {
                if ( input[ i ] == ' ' )
                {
                    continue;
                }

                if ( statusLine.Protocol == null )
                {
                    while ( i < input.Length && input[i] != '/' )
                    {
                        var character = input[i++];

                        if ( character != ' ' )
                        {
                            builder.Append( character );
                        }
                    }

                    statusLine.Protocol = builder.ToString();
                }
                else if ( statusLine.Version == null )
                {
                    while ( i < input.Length && input[i] != ' ')
                    {
                        builder.Append( input[i++] );
                    }

                    statusLine.Version = builder.ToString();
                }
                else if ( statusLine.Code == null )
                {
                    while ( i < input.Length && input[i] != ' ' )
                    {
                        builder.Append( input[i++] );
                    }

                    statusLine.Code = builder.ToString();
                }
                else if ( statusLine.Reason == null )
                {
                    while ( i < input.Length )
                    {
                        builder.Append( input[i++] );
                    }

                    statusLine.Reason = builder.ToString();
                }

                builder.Clear();
            }

            result = string.IsNullOrEmpty( statusLine.Protocol )
                  || string.IsNullOrEmpty( statusLine.Version )
                  || string.IsNullOrEmpty( statusLine.Code ) ? null : statusLine;

            return true;
        }



        public override string ToString()
        {
            // we are at the low level, do not add null empty checks
            return $"{Protocol}/{Version} {Code} {Reason}";
        }
    }
}
