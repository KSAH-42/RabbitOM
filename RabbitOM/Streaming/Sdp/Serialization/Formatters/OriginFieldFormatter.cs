using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    public static class OriginFieldFormatter
    {
        public static string Format(OriginField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            var userName = !string.IsNullOrWhiteSpace(field.UserName) ? field.UserName : "-";

            builder.AppendFormat("{0} ", userName);
            builder.AppendFormat("{0} ", field.SessionId);
            builder.AppendFormat("{0} ", field.Version);
            builder.AppendFormat("{0} ", DataConverter.ConvertToString(field.NetworkType));
            builder.AppendFormat("{0} ", DataConverter.ConvertToString(field.AddressType));
            builder.AppendFormat("{0} ", field.Address);

            return builder.ToString();
        }

        public static bool TryParse(string value, out OriginField result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = DataConverter.ReArrange( value , '/' ).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = new OriginField()
            {
                UserName    = tokens.ElementAtOrDefault(0) ,
                SessionId   = DataConverter.ConvertToULong( tokens.ElementAtOrDefault(1) ),
                Version     = DataConverter.ConvertToULong( tokens.ElementAtOrDefault(2) ),
                NetworkType = DataConverter.ConvertToNetworkType( tokens.ElementAtOrDefault(3) ),
                AddressType = DataConverter.ConvertToAddressType( tokens.ElementAtOrDefault(4) ),
                Address     = tokens.ElementAtOrDefault(5) ,
            };

            return true;
        }
    }
}
