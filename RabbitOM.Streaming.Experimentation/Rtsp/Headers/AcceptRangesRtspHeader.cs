using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptRangesRtspHeader
    {
        public static readonly string TypeName = "Accept-Ranges";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        



        public bool Bytes { get; set; }
       
        public bool Ntp { get; set; }

        public bool Smpte { get; set; }

        public bool Clock { get; set; }

        public bool Utc { get; set; }
        



        
        
        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( Bytes )
            {
                builder.Append( "bytes, ");
            }

            if ( Clock )
            {
                builder.Append( "clock, ");
            }

            if ( Ntp )
            {
                builder.Append( "ntp, ");
            }

            if ( Smpte )
            {
                builder.Append( "smpte, ");
            }

            if ( Utc )
            {
                builder.Append( "utc, ");
            }

            return builder.ToString().Trim( ' ' , ',' );
        }
        



        
        
        public static bool TryParse( string input , out AcceptRangesRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                var header = new AcceptRangesRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( ValueComparer.Equals( "bytes" , token ) )
                    {
                        header.Bytes = true;
                    }
                    else if ( ValueComparer.Equals( "ntp" , token ) )
                    {
                        header.Ntp = true;
                    }
                    else if ( ValueComparer.Equals( "smpte" , token ) )
                    {
                        header.Smpte = true;
                    }
                    else if ( ValueComparer.Equals( "utc" , token ) )
                    {
                        header.Utc = true;
                    }
                    else if ( ValueComparer.Equals( "clock" , token ) )
                    {
                        header.Clock = true;
                    }
                }

                result = header;
            }

            return result != null;
        }
    }
}
