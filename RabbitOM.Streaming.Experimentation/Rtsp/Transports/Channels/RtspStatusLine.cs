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

            // here we don't use string.split at lower level
            // the perf result show signatificative improvement

            // RTSP/1.0 555 Doom-Patrol Dark-Series 

            var startLine = new RtspStatusLine();
            var builder = new StringBuilder();

            for ( var i = 0 ; i < input.Length ; ++ i )
            {
                var element = input[ i ];

                if ( element == ' ' || element == '/' )
                {
                    if ( builder.Length > 0 )
                    {
                        if ( startLine.Protocol == null )
                        {
                            startLine.Protocol = builder.ToString();
                        }
                        else if ( startLine.Version == null )
                        {
                            startLine.Version = builder.ToString();
                        }
                        else if ( startLine.Code == null )
                        {
                            startLine.Code = builder.ToString();
                        }
                        else
                        {
                            builder.Append( element );

                            if ( i + 1 >= input.Length )
                            {
                                startLine.Reason = builder.ToString();
                            }

                            continue;
                        }

                        builder.Clear();
                    }
                }
                else
                {
                    builder.Append( element );

                    if ( i + 1 >= input.Length )
                    {
                        startLine.Reason = builder.ToString();
                    }
                }
            }

            result = string.IsNullOrEmpty( startLine.Protocol )
                  || string.IsNullOrEmpty( startLine.Version )
                  || string.IsNullOrEmpty( startLine.Code ) ? null : startLine;

            return true;
        }





        // we are at the low level, do not add null empty checks
        public override string ToString()
        {
            return $"{Protocol}/{Version} {Code} {Reason}";
        }
    }
}
