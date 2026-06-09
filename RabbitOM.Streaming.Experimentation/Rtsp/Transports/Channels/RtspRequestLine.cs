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



        // input: DESCRIBE    rtsp://1.1.1.1/predestination    RTSP / 1.0

        public static bool TryParse( string input , out RtspRequestLine result )
        {
            result = null;

            if ( string.IsNullOrEmpty( input ) )
            {
                return false;
            }

            // we don't use string.split here, even are regular expression

            var requestLine = new RtspRequestLine();
            var builder = new StringBuilder(100);
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

            if ( string.IsNullOrEmpty( requestLine.Method ) || string.IsNullOrEmpty( requestLine.Uri ) || string.IsNullOrEmpty( requestLine.Protocol ) || string.IsNullOrEmpty( requestLine.Version ) )
            {
                return false;
            }

            result = requestLine;

            return true;
        }




        public override string ToString()
        {
            return $"{Method} {Uri} {Protocol}/{Version}";
        }
    }
}
