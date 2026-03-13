using System;
using System.Collections.Generic;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{

    public sealed class RtspRequestMessage
    {
        public RtspMethod Method { get; }
        public Version Version { get; }
        public string Uri { get; }
        public IReadOnlyDictionary<string,string> Headers { get; }
        public Stream Body { get; }
    }
}
