using System;
using System.Collections.Generic;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{

    public class RtspRequestMessage
    {
        public RtspMethod Method { get; set; }
        public Version Version { get; set; }
        public string Uri { get; set; }
        public IReadOnlyDictionary<string,string> Headers { get; set; }
        public Stream Body { get; set; }
    }
}
