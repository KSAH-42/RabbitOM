using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public sealed class TrimWithRemoveAllQuotesValueNormalizer : StringValueNormalizer
    {
        public override string Normalize( string value )
        {
            return value?
                .Replace( "\'" , "" ) 
                .Replace( "\"" , "" ) 
                .Replace( "`" , "" )
                .Trim()
                ?? string.Empty;
        }
    }
}
