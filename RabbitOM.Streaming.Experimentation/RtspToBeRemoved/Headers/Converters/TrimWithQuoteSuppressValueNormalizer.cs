using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers
{
    public sealed class TrimWithQuoteSuppressValueNormalizer : StringValueNormalizer
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
