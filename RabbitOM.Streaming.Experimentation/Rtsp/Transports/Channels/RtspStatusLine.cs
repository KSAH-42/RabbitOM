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

            // here we don't use string.split, the result show signatificative improvement 

            // RTSP / 1.0 555 Doom-Patrol

            var statusLine = new RtspStatusLine();
            var builder = new StringBuilder();
            
            for ( var i = 0 ; i < input.Length ; ++ i )
            {
                var element = input[ i ];

                if ( element == ' ' || element == '/' && statusLine.Version == null )
                {
                    if ( builder.Length > 0 )
                    {
                        Populate( statusLine , builder.ToString() );

                        builder.Clear();
                    }
                }
                else
                {
                    builder.Append( element );
                }
            }

            if ( builder.Length > 0 )
            {
                Populate( statusLine , builder.ToString() );

                builder.Clear();
            }

            if ( string.IsNullOrEmpty( statusLine.Protocol ) || string.IsNullOrEmpty( statusLine.Version ) || string.IsNullOrEmpty( statusLine.Code ) )
            {
                return false;
            }

            result = statusLine;

            return true;
        }

        private static void Populate( RtspStatusLine line , string token )
        {
            if ( line.Protocol == null )
            {
                line.Protocol = token;
            }
            else if ( line.Version == null )
            {
                line.Version = token;
            }
            else if ( line.Code == null )
            {
                line.Code = token;
            }
            else if ( line.Reason == null )
            {
                line.Reason = token;
            }
        }






        // we are at the low level, do not add null empty checks
        public override string ToString()
        {
            return $"{Protocol}/{Version} {Code} {Reason}";
        }
    }
}
