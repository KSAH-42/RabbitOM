using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderParserFactory
    {
        public static IEnumerable<RtspHeaderParser> CreateRequestsParsersFrom<T>()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<RtspHeaderParser> CreateResponsesParsersFrom<T>()
        {
            throw new NotImplementedException();
        }
    }
}
