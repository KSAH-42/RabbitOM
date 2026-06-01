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





        // here we don't use string.split at lower level
        // the perf result show signatificative improvement

        //  input:"DESCRIBE rtsp://1.1.1.1/predestination RTSP/1.0"

            
        public static bool TryParse( string input , out RtspRequestLine result )
        {
            result = null;

            if ( string.IsNullOrEmpty( input ) )
            {
                return false;
            }

            var startLine = new RtspRequestLine();
            var builder = new StringBuilder();
            var step = 0;
            var i = -1;

            while ( ++ i < input.Length )
            {
                if ( input[ i ] == ' ' ) { continue; }

                switch( step ++ )
                {
                    case 0:

                        while ( i < input.Length && input[i] != ' ' )
                        {
                            builder.Append( input[i++] );
                        }
                        
                        startLine.Method = builder.ToString();

                        break;

                    case 1:

                        while ( i < input.Length && input[i] != ' ' )
                        {
                            builder.Append( input[i++] );
                        }

                        startLine.Uri = builder.ToString();

                        break;

                    case 2:

                        while ( i < input.Length && input[i] != '/' )
                        {
                            var character = input[i++];

                            if ( character != ' ' )
                            {
                                builder.Append( character );
                            }
                        }

                        startLine.Protocol = builder.ToString();

                        break;

                    case 3:

                        while ( i < input.Length )
                        {
                            if ( input[i] != ' ' )
                            {
                                builder.Append( input[i] );
                            }

                            i++;
                        }

                        startLine.Version = builder.ToString();

                        break;
                }

                builder.Clear();
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
