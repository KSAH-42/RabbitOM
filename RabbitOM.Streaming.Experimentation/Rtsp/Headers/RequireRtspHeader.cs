using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RequireRtspHeader : RtspHeader
    {   
        public const string TypeName = "Require";

        


        private string _value;
        




        public string Value
        {
            get => _value ?? string.Empty;
            set => _value = value;
        }


        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _value );
        }

        public override string ToString()
        {
            return _value ?? string.Empty;
        }
        

        public static bool TryParse( string value , out RequireRtspHeader result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RequireRtspHeader() { Value = value };

            return true;
        }
    }
}
