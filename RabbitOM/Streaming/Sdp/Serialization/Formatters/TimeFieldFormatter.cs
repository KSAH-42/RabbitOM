using System;
using System.Linq;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    public static class TimeFieldFormatter
    {
        public static string Format(TimeField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            return string.Format( "{0} {1}", field.StartTime, field.StopTime);
        }

        public static bool TryParse(string value, out TimeField result)
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = new TimeField()
            {
                StartTime = DataConverter.ConvertToLong( tokens.ElementAtOrDefault(0) ),
                StopTime  = DataConverter.ConvertToLong( tokens.ElementAtOrDefault(1) ),
            };

            return true;
        }
    }
}
