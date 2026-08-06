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

            if ( port < 0 )
            {
                return new ValidationResult( false , "Negative value is not allowed" );
            }

            return ValidationResult.ValidResult;
        }
    }

}
