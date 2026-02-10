using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class BandwithRtspHeader : RtspHeader
    {
        public const string TypeName = "Bandwith";
        


        public long BitRate { get; set; }



        public override bool TryValidate()
        {
            return BitRate > 0;
        }

        public override string ToString()
        {
            return BitRate.ToString();
        }




        
        public static bool TryParse( string value , out BandwithRtspHeader result )
        {
            result = null;

            if ( ! long.TryParse( StringRtspNormalizer.Normalize( value ) , out var bitRate ) )
            {
                return false;
            }

            result = new BandwithRtspHeader() { BitRate = bitRate };

            return true;
        }
    }
}
