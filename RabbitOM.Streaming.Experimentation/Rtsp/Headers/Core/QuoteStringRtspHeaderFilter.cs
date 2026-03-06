using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core
{
    public sealed class QuoteStringRtspHeaderFilter : StringRtspHeaderFilter
    {
        private static readonly char QuoteChars = '\"' ;

        public override string Filter( string value )
        {
            return $"{QuoteChars}{ value?.Trim() ?? string.Empty }{QuoteChars}";
        }
    }
}
