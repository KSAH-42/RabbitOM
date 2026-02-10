using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class ContentLengthRtspHeader : RtspHeader 
    {
        public const string TypeName = "Content-Length";
        


        private long _value;



        public long Value
        {
            get => _value;
            set => _value = value;
        }



        public override bool TryValidate()
        {
            return _value >= 0;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
        






        public static bool TryParse( string value , out ContentLengthRtspHeader result )
        {
            result = null;

            if ( ! long.TryParse( value , out var number ) )
            {
                return false;
            }

            result = new ContentLengthRtspHeader() { Value = number };

            return true;
        }
    }
}
