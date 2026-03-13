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


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public static bool TryParse( string input , out RtspResponseMessage result )
        {
            throw new NotImplementedException();
        }
    }
}
