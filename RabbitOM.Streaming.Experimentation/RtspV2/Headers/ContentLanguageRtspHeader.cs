using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class ContentLanguageRtspHeader : RtspHeader 
    {
        public const string TypeName = "Content-Language";
        
        
        
        
        
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
        





        public static bool TryParse( string value , out ContentLanguageRtspHeader result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new ContentLanguageRtspHeader() { Value = value };

            return true;
        }
    }
}
