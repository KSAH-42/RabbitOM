using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RateControlRtspHeaderValue
    {
        private readonly static StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;

        public bool IsEnabled { get; set; }

        public static bool TryParse( string input , out RateControlRtspHeaderValue result )
        {
            result = null;

            var value = RtspHeaderValueSanitizer.UnQuotesWithTrim( input );

            if ( ValueComparer.Equals( value , "yes" ) )
            {
                result = new RateControlRtspHeaderValue() { IsEnabled = true };
            }
            else if ( ValueComparer.Equals( value , "no" ) )
            {
                result = new RateControlRtspHeaderValue() { IsEnabled = false };
            }

            return result != null;
        }

        public override string ToString()
        {
            return IsEnabled ? "yes" : "no";
        }
    }
}
