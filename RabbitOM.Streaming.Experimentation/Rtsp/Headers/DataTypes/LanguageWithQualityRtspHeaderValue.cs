using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class LanguageWithQualityRtspHeaderValue
    {
        private static readonly Regex ValueRegularExpression = new Regex (@"^[A-Za-z]{2,8}(-[A-Za-z0-9]{1,8})*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public LanguageWithQualityRtspHeaderValue( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( ! ValueRegularExpression.IsMatch( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            Value = value;
        }

        public LanguageWithQualityRtspHeaderValue( string value , double quality )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( ! ValueRegularExpression.IsMatch( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            Value = value;
            Quality = quality;
        }




        public string Value { get; }

        public double? Quality { get; }




        public static bool TryParse( string input , out LanguageWithQualityRtspHeaderValue result )
        {
            result = null;

            throw new NotImplementedException();
        }





        public override string ToString()
        {
            return Quality.HasValue ? $"{Value}; q={Quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo)}" : Value;
        }
    }
}
