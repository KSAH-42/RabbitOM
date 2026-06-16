using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    internal static class RtspStatusCodeChecker
    {
        public static bool IsSuccessStatusCode( RtspStatusCode value )
        {
            var code = (int) value;

            return 200 <= code && code <= 299;
        }
    }
}
