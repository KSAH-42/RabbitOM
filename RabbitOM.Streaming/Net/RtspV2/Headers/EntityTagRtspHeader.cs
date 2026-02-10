using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class EntityTagRtspHeader : RtspHeader 
    {
        public const string TypeName = "ETag";
        


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
        



        public static bool TryParse( string value , out EntityTagRtspHeader result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new EntityTagRtspHeader() { Value = value };

            return true;
        }
    }
}
