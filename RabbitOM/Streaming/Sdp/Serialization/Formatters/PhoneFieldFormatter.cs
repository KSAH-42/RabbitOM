using System;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    public static class PhoneFieldFormatter
    {
        public static string Format(PhoneField field)
        {
            return string.Format("{0}" , field?.Value ?? string.Empty );
        }

        public static bool TryParse(string value, out PhoneField result)
        {
            result = default;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new PhoneField()
            {
                Value = value
            };

            return true;
        }
    }
}
