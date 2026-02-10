using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class AcceptLanguageRtspHeader : RtspHeader 
    {
        public const string TypeName = "Accept-Language";

        public string Value { get; private set; } = string.Empty;

        public void SetValue( string value )
        {
            Value = StringRtspNormalizer.Normalize( value );
        }

        public override bool TryValidate()
        {
            return StringRtspValidator.TryValidate( Value );
        }

        public override string ToString()
        {
            return Value;
        }
        
        public static bool TryParse( string value , out AcceptLanguageRtspHeader result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new AcceptLanguageRtspHeader();

            result.SetValue( value );

            return true;
        }
    } 
}
