using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    public sealed class FramesRtspHeaderValue
    {
        public const string IntraType = "intra";
        public const string PredicatedType = "predicated";
        public const string AllType = "all";

        private string _type = string.Empty;

        public string Value
        {
            get => _type;
            set => _type = RtspHeaderValueValidator.EnsureWellFormed( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public static bool TryParse( string input , out FramesRtspHeaderValue result )
        {
            result = null;

            var value = RtspHeaderValueSanitizer.TrimWithRemoveAllQuotes( input );

            if ( RtspHeaderValueValidator.IsWellFormed( value , RtspHeaderValueValidatorCharSet.BasicToken ) )
            {
                result = new FramesRtspHeaderValue() { _type = value };
            }

            return result != null;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
