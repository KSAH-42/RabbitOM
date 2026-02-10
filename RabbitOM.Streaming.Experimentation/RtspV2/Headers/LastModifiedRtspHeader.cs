using System;
using System.Globalization;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class LastModifiedRtspHeader : RtspHeader 
    {
        private const string FormatDate = "ddd, dd MMM yyyy HH:mm:ss GMT";

        public const string TypeName = "Last-Modified";
        




        private DateTime _value;



        public DateTime Value
        {
            get => _value;
            set => _value = value;
        }



        public override bool TryValidate()
        {
            return DateTime.MinValue < _value && _value < DateTime.MaxValue;
        }

        public override string ToString()
        {
            return _value.ToUniversalTime().ToString( FormatDate , CultureInfo.InvariantCulture );
        }
        



        public static LastModifiedRtspHeader Parse( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return TryParse( value , out var result ) ? result : throw new FormatException();
        }

        public static bool TryParse( string value , out LastModifiedRtspHeader result )
        {
            result = null;

            if ( ! DateTime.TryParse( value , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var date ) )
            {
                return false;
            }

            result = new LastModifiedRtspHeader() { Value = date };

            return true;
        }
    }
}
