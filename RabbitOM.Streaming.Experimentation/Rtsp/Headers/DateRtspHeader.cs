using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class DateRtspHeader
    {
        public static readonly string TypeName = "Date";



        
        public DateTime Value { get; set; }




        public override string ToString()
        {
            return RtspHeaderParser.Format( Value );
        }




        public static bool TryParse( string input , out DateRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , out var value )
                ? new DateRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
