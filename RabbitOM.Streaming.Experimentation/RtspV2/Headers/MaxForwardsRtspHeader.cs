using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class MaxForwardsRtspHeader : RtspHeader 
    {
        public const string TypeName = "Max-Forwards";
        




        private int _value;
        




        public int Value
        {
            get => _value;
            set => _value = value;
        }




        public override bool TryValidate()
        {
            return true;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
        


        public static bool TryParse( string value , out MaxForwardsRtspHeader result )
        {
            result = null;

            if ( ! int.TryParse( value , out var number ) )
            {
                return false;
            }

            result = new MaxForwardsRtspHeader() { Value = number };

            return true;
        }
    }
}
