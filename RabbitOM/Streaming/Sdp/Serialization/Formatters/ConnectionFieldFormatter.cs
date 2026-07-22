using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Sdp.Serialization.Formatters
{
    public static class ConnectionFieldFormatter
    {
        public static string Format(ConnectionField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            var address = DataConverter.ConvertToLoopBackIfEmpty(field.Address, field.AddressType);

            builder.AppendFormat( "{0} {1} {2}"
                , DataConverter.ConvertToString(field.NetworkType)
                , DataConverter.ConvertToString(field.AddressType)
                , address 
                );

            if ( ! string.IsNullOrWhiteSpace( address ) )
            {
                builder.AppendFormat( "/{0}" , field.TTL );
            }

            return builder.ToString();
        }

        public static bool TryParse(string value, out ConnectionField result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = DataConverter.ReArrange( value , '/' ).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if ( tokens.Length < 3 )
            {
                return false;
            }

            result = new ConnectionField()
            {
                NetworkType = DataConverter.ConvertToNetworkType( tokens.ElementAtOrDefault(0) ),
                AddressType = DataConverter.ConvertToAddressType( tokens.ElementAtOrDefault(1) ),
                Address     = tokens.ElementAtOrDefault(2),
                TTL         = DataConverter.ConvertToByteFromTTLFormat( tokens.ElementAtOrDefault(2) ),
            };

            return true;
        }
    }
}
