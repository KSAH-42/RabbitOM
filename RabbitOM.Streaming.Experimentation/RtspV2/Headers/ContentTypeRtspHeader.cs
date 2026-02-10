using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class ContentTypeRtspHeader : RtspHeader 
    {
        public const string TypeName = "Content-Type";

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
        
        public static ContentTypeRtspHeader Parse( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return TryParse( value , out var result ) ? result : throw new FormatException();
        }

        public static bool TryParse( string value , out ContentTypeRtspHeader result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new ContentTypeRtspHeader() { Value = value };

            return true;
        }
    }
}
