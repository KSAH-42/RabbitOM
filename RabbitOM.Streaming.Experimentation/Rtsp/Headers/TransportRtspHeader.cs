using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class TransportRtspHeader : RtspHeader 
    {
        public const string TypeName = "Transport";
        
        
        public string TransportType { get; set; }
        public string TransmissionType { get; set; }
        public byte? TTL { get; set; }
        public byte? Layers { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string SSRC { get; set; }
        public string Mode { get; set; }
        public PortPair? Port { get; set; }
        public PortPair? ClientPort { get; set; }
        public PortPair? ServerPort { get; set; }
        public PortPair? InterLeavedPort { get; set; }

        
     
        public override bool TryValidate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }





        public static bool TryParse( string value , out TransportRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
