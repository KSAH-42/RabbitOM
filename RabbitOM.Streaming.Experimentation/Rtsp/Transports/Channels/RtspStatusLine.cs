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

            // XXX1XXXXXX2222XXXXX2XXX

            var startLine = new RtspStatusLine();
            var builder = new StringBuilder();
            var step = 0;
            var i = -1;

            while ( ++ i < input.Length )
            {
                if ( input[ i ] == ' ' ) { continue; }

                switch( step ++ )
                {
                    case 0:

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

                    case 1:

                        while ( i < input.Length && input[i] != ' ')
                        {
                            builder.Append( input[i++] );
                        }

                        startLine.Version = builder.ToString();

                        break;

                    case 2:

                        while ( i < input.Length && input[i] != ' ' )
                        {
                            builder.Append( input[i++] );
                        }
                        
                        startLine.Code = builder.ToString();

                        break;

                    case 3:

                        while ( i < input.Length )
                        {
                            builder.Append( input[i++] );
                        }

                        startLine.Reason = builder.ToString();

                        break;
                }

                builder.Clear();
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
