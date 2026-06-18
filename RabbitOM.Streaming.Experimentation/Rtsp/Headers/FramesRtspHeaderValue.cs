using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class FramesRtspHeaderValue
    {
        public const string IntraType = "intra";
        public const string PredicatedType = "predicated";
        public const string AllType = "all";

        private string _type = string.Empty;

        public string Type
        {
            get => _type;
            set => _type = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public static bool TryParse( string input , out FramesRtspHeaderValue result )
        {
            result = null;

            var value = RtspHeaderValueSanitizer.TrimWithRemoveAllQuotes( input );

            if ( RtspHeaderValueValidator.IsWellFormedToken( value ) )
            {
                result = new FramesRtspHeaderValue() { _type = value };
            }

            return result != null;
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
