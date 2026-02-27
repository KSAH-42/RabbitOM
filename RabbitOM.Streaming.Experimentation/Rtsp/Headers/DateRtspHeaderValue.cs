using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class DateRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Date";



        
        public DateTime Value { get; set; }




        public override string ToString()
        {
            return DateTimeRtspHeaderParser.Format( Value );
        }




        public static bool TryParse( string input , out DateRtspHeaderValue result )
        {
            result = DateTimeRtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out DateTime value )
                ? new DateRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
