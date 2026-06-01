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

            // here we don't use string.split at lower level
            // the perf result show signatificative improvement

            //  input:" DESCRIBE     rtsp://1.1.1.1/predestination    RTSP  / 1.0  "

            var startLine = new RtspRequestLine();
            var builder = new StringBuilder();

            for ( var i = 0 ; i < input.Length ; ++ i )
            {
                var element = input[ i ];

                if ( element == ' ' || element == '/' && startLine.Uri != null )
                {
                    if ( builder.Length > 0 )
                    {
                        if ( startLine.Method == null )
                        {
                            startLine.Method = builder.ToString();
                        }
                        else if ( startLine.Uri == null )
                        {
                            startLine.Uri = builder.ToString();
                        }
                        else if ( startLine.Protocol == null )
                        {
                            startLine.Protocol = builder.ToString();
                        }
                        else if ( startLine.Version == null )
                        {
                            builder.Append( element );

                            if ( i + 1 >= input.Length )
                            {
                                startLine.Version = builder.ToString();
                            }

                            continue;
                        }

                        builder.Clear();
                    }
                }
                else
                {
                    builder.Append( element );
                }
            }

            result = string.IsNullOrEmpty( startLine.Method )
                  || string.IsNullOrEmpty( startLine.Uri )
                  || string.IsNullOrEmpty( startLine.Protocol )
                  || string.IsNullOrEmpty( startLine.Version ) ? null : startLine;

            return result != null;
        }






        // we are at the low level, do not add null empty checks
        public override string ToString()
        {
            return $"{Method} {Uri} {Protocol}/{Version}";
        }
    }
}
