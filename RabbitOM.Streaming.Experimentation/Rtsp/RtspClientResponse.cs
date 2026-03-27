using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    public sealed class RtspClientResponse 
    {
        public RtspStatusCode Status { get; }
        
        public HeaderCollectionResponse Headers { get; } = new HeaderCollectionResponse();
        
        public MemoryStream Body { get; } = new MemoryStream();
    }
}
