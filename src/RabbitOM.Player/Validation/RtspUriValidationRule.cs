using System;
using System.Globalization;
using System.Windows.Controls;

namespace RabbitOM.Player.Validation
{
    public sealed class RtspUriValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ( ! Uri.TryCreate( value as string , UriKind.Absolute , out var uri ) )
            {
                return new ValidationResult( false , "Bad format" );
            }

            if ( uri.Scheme != "rtsp" )
            {
                return new ValidationResult( false , "Bad scheme" );
            }

            if ( string.IsNullOrEmpty( uri.Host ) )
            {
                return new ValidationResult( false , "No host has been specified" );
            }

            return ValidationResult.ValidResult;
        }
    }
}
