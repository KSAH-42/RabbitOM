using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers.Normalizers
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
