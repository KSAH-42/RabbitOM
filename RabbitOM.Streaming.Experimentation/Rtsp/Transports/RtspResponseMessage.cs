using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspResponseMessage
    {        
        public string Method { get; set; }
        
        public string Version { get; set; }
        
        public string ReasonPhase { get; set; }

        public int StatusCode { get; set; }

        public NameValuesRtspCollection Headers { get; set; }

        public byte[] Body { get; set; }

        




        public static bool IsNullOrInvalid( RtspResponseMessage message )
        {
            throw new NotImplementedException();
        }

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
