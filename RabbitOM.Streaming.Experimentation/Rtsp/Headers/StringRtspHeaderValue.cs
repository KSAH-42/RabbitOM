using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class StringRtspHeaderValue : RtspHeaderValue
    {
        private string _value = string.Empty;





        public StringRtspHeaderValue()
        {
        }

        public StringRtspHeaderValue( string value )
        {
            Value = value;
        }








        public string Value
        {
            get => _value;
            set => _value = value?.Trim() ?? string.Empty;
        }









        public static implicit operator StringRtspHeaderValue( string value )
        {
            return new StringRtspHeaderValue( value );
        }

        public static bool TryParse( string input , out StringRtspHeaderValue result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            result = new StringRtspHeaderValue( input );

            return true;
        }







        public override string ToString()
        {
            return Value;
        }
    }
}
