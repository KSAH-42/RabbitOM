using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class ExpiresRtspHeader : RtspHeader 
    {
        private const string FormatDate = "ddd, dd MMM yyyy HH:mm:ss GMT";

        public const string TypeName = "Expires";
        


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
        



        public static bool TryParse( string value , out ExpiresRtspHeader result )
        {
            result = null;

            if ( ! DateTime.TryParse( value , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var date ) )
            {
                return false;
            }

            result = new ExpiresRtspHeader() { Value = date };

            return true;
        }
    }
}
