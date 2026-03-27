using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers
{
    public sealed class StringRtspHeaderValue : RtspHeaderValue
    {
        public StringRtspHeaderValue( string value ) => Value = value?.Trim() ?? string.Empty;


        public string Value { get; }

        
        public static implicit operator StringRtspHeaderValue( string value ) => new StringRtspHeaderValue( value );
        
        public static bool TryParse( string input , out StringRtspHeaderValue result )
        {
            result = string.IsNullOrWhiteSpace( input ) ? null : new StringRtspHeaderValue( input );
            
            return result != null;
        }


        public override string ToString() => Value;
    }
}
