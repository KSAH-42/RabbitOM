using System;
using System.Net;
using System.Globalization;
using System.Windows.Controls;

namespace RabbitOM.Player.Validation
{
    public sealed class MulticastIPAddressValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ( ! IPAddress.TryParse( value as string , out IPAddress ip ) )
            {
                return new ValidationResult( false , "Bad format" );
            }

            if ( ip.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork )
            {
                return new ValidationResult( false , "Bad format" );
            }

            var bytes = ip.GetAddressBytes();

            if ( bytes[0] < 224 || bytes[0] > 239 ) // ipv4 only
            {
                return new ValidationResult( false , "Invalid range" );
            }

            return ValidationResult.ValidResult;
        }
    }

}
