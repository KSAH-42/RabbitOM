using System;
using System.Text;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    public sealed class RtspStatusLine
    {
        public string Protocol { get; set; }

        public string Version { get; set; }

        public string Code { get; set; }

        public string Reason { get; set; }




        // RTSP/1.0 555 Doom-Patrol | Dark-Series

        // protocol     version  code    <------- reason -------->
        //   RTSP    /    1.0    555     Doom-Patrol | Dark-Series

        public static bool TryParse( string input , out RtspStatusLine result )
        {
            result = null;

            if ( string.IsNullOrEmpty( input ) )
            {
                return false;
            }

            var statusLine = new RtspStatusLine();
            var builder = new StringBuilder( input.Length );
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
                    while ( i < input.Length && input[i] != ' ' )
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

            if ( string.IsNullOrEmpty( statusLine.Protocol ) || string.IsNullOrEmpty( statusLine.Version ) || string.IsNullOrEmpty( statusLine.Code ) )
            {
                return false;
            }

            result = statusLine;

            return true;
        }



        public override string ToString()
        {
            return $"{Protocol}/{Version} {Code} {Reason}";
        }
    }
}
