using System;
using System.Linq;

namespace RabbitOM.Net.Sdp.Serialization
{
    public static class EmailFieldFormatter
    {
        public static string Format(EmailField field)
        {
            if (field == null)
            {
                return string.Empty;
            }

            if ( string.IsNullOrWhiteSpace(field.Name) )
            {
                return field.Address;
            }

            return string.Format( "{0} ({1})" , field.Address , field.Name );
        }

        // TODO: include a validation
        // kader.sahnine11@gmail.com should be ok
        // a.sahnine@netcourrier.com should be ok
        // https://github.com/KSAH-42/RabbitOM should failed
        public static bool TryParse(string value, out EmailField result)
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

            result = new EmailField()
            {
                Address = tokens.ElementAtOrDefault(0),
                Name    = string.Join( " " , tokens.Skip(1) ),
            };

            return true;
        }
    }
}
