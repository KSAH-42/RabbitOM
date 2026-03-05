using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers
{
    public static class LongRtspHeaderParser
    {
        private static readonly char[] SpaceWithQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public static bool TryParse( string input , out long result )
        {
            return long.TryParse( input?.Trim( SpaceWithQuotesChars ) , out result );
        }
    }
}
