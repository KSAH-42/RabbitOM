using System;
using System.Globalization;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    public sealed class TimeStampRtspHeaderValue
    {
        public TimeStampRtspHeaderValue( DateTime dateTime )
        {
            Time = dateTime;
        }

        public TimeStampRtspHeaderValue( TimeSpan delay )
        {
            Delay = delay;
        }



        public DateTime? Time { get; }

        public TimeSpan? Delay { get; }




        public static bool TryParse( string input , out TimeStampRtspHeaderValue result )
        {
            result = null;

            input = RtspHeaderValueSanitizer.UnQuotesWithTrim( input );

            if ( ! string.IsNullOrWhiteSpace( input ) )
            {
                if ( System.DateTime.TryParse( input , CultureInfo.InvariantCulture , DateTimeStyles.AdjustToUniversal , out var dateTime ) )
                {
                    result = new TimeStampRtspHeaderValue( dateTime );
                }
                else 

                if ( TimeSpan.TryParse( input , out var timeSpan ) )
                {
                    result = new TimeStampRtspHeaderValue( timeSpan );
                }
            }

            return result != null;
        }



        public override string ToString()
        {
            if ( Delay.HasValue )
            {
                return Delay.ToString();
            }

            if ( Time.HasValue )
            {
                return ( Time.Value.Kind == DateTimeKind.Local ? Time.Value.ToUniversalTime() : Time.Value ).ToString( "r" , CultureInfo.InvariantCulture);
            }

            return string.Empty;
        }
    }
}
