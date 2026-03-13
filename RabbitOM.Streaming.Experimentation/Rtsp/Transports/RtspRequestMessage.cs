using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspRequestMessage
    {        
        public string Method { get; set; }

        public string Version { get; set; }

        public string Uri { get; set; }

        public IDictionary<string,string> Headers { get; } = new Dictionary<string,string>( StringComparer.OrdinalIgnoreCase );

        public byte[] Body { get; set; }


        

        public static bool TryParse( in ArraySegment<byte> input , out RtspRequestMessage result )
        {
            throw new NotImplementedException();
        }



        public byte[] ToArray()
        {
            throw new NotImplementedException();
        }
    }
}
