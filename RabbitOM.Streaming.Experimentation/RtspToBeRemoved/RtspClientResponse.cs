using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers;
    
    public sealed class RtspClientResponse 
    {
        public RtspStatusCode Status { get; }
        
        public RtspResponseHeaderCollection Headers { get; } = new RtspResponseHeaderCollection();
        
        public MemoryStream Body { get; } = new MemoryStream();
    }
}
