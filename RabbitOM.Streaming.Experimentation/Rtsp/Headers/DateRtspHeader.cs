using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class DateRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Date";



        
        public DateTime Value { get; set; }




        public override string ToString()
        {
            return RtspHeaderParser.Formatter.Format( Value );
        }




        public static bool TryParse( string input , out DateRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , out DateTime value )
                ? new DateRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
