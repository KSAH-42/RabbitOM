using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers
{
    public static class ByteRtspHeaderParser
    {
        private static readonly char[] SpaceWithQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public static bool TryParse( string input , out byte result )
        {
            return byte.TryParse( input?.Trim( SpaceWithQuotesChars ) , out result );
        }
    }
}
