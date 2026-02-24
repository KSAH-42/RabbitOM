using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptRangesRtspHeader
    {
        public static readonly string TypeName = "Accept-Ranges";
        




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

            return builder.ToString().Trim( ',' , ' ' );
        }







        public static bool TryParse( string input , out AcceptRangesRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new AcceptRangesRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( string.Equals( "bytes" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.Bytes = true;
                    }
                    else if ( string.Equals( "ntp" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.Ntp = true;
                    }
                    else if ( string.Equals( "smpte" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.Smpte = true;
                    }
                    else if ( string.Equals( "utc" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.Utc = true;
                    }
                    else if ( string.Equals( "clock" , token , StringComparison.OrdinalIgnoreCase ) )
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
