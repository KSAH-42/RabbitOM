using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspResponseMessage
    {        
        public string Method { get; set; }
        
        public string Version { get; set; }
        
        public string ReasonPhase { get; set; }

        public int StatusCode { get; set; }

        public IDictionary<string,string> Headers { get; } = new Dictionary<string,string>( StringComparer.OrdinalIgnoreCase );

        public byte[] Body { get; set; }

        




        public static bool TryParse( in ArraySegment<byte> input , out RtspResponseMessage result )
        {
            throw new NotImplementedException();
        }






        public byte[] ToArray()
        {
            throw new NotImplementedException();
        }
    }
}
