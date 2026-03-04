using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class SpeedRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Speed";



        
        public long Value { get; set; }




        public override string ToString()
        {
            return Value.ToString();
        }





        public static bool TryParse( string input , out SpeedRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , out long value )
                ? new SpeedRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
