using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved
{
    using RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers;
    
    public sealed class RtspClientResponse 
    {
        public RtspStatusCode Status { get; }
        
        public RtspResponseHeaderCollection Headers { get; } = new RtspResponseHeaderCollection();
        
        public MemoryStream Body { get; } = new MemoryStream();
    }
}
