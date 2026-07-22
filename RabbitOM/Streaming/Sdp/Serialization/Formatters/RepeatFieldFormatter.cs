using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    public static class RepeatFieldFormatter
    {
        public static string Format(RepeatField field)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("{0} ", field.RepeatInterval.StartTime);
            builder.AppendFormat("{0} ", field.RepeatInterval.StopTime);
            builder.AppendFormat("{0} ", field.ActiveDuration.StartTime);
            builder.AppendFormat("{0} ", field.ActiveDuration.StopTime);

            return builder.ToString();
        }

        public static bool TryParse(string value, out RepeatField result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = new RepeatField()
            {
                RepeatInterval = new ValueTime( DataConverter.ConvertToLong(tokens.ElementAtOrDefault(0) ), DataConverter.ConvertToLong(tokens.ElementAtOrDefault(1) ) ),
                ActiveDuration = new ValueTime( DataConverter.ConvertToLong(tokens.ElementAtOrDefault(2) ), DataConverter.ConvertToLong(tokens.ElementAtOrDefault(3) ) ),
            };

            return true;
        }
    }
}
