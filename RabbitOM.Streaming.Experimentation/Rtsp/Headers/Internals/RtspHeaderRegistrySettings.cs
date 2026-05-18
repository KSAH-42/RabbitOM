using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderRegistrySettings
    {
        public IReadOnlyDictionary<string,object> RegisteredParsers { get; }

        public IReadOnlyCollection<string> ForbiddenHeaders { get; }
    }
}
