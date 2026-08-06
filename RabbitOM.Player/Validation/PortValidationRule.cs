using System;
using System.Globalization;
using System.Windows.Controls;

namespace RabbitOM.Player.Validation
{
    public class PortValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ( ! int.TryParse( value as string , out var port ) )
            {
                return new ValidationResult( false , "Bad format" );
            }

            // please note that a socket bind to a port equal to zero is valid but here for ux reason we just reject this value
            if ( port <= 0 || port > ushort.MaxValue )
            {
                return new ValidationResult( false , "Bad value" );
            }

            return ValidationResult.ValidResult;
        }
    }

}
