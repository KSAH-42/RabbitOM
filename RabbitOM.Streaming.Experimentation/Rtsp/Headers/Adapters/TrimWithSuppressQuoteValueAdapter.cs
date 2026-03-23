using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters
{
    public sealed class TrimWithSuppressQuoteValueAdapter : StringValueAdapter
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public override string Adapt( string value )
        {
            return value?.Trim()
                .Replace( "\'" , "" ) 
                .Replace( "\"" , "" ) 
                .Replace( "`" , "" ) 
                ?? string.Empty;
        }
    }
}
