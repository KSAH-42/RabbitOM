using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public abstract class RtcpPacket
    {
        public const byte DefaultVersion = 2;

        protected RtcpPacket() : this( DefaultVersion )
        {
        }

        protected RtcpPacket( byte version )
        {
            Version = version;

#if !DEBUG
            throw new NotImplementedException( "do not used this implementation, because it is not yet finished" );
#endif
        }

        public byte Version { get; private set; }
    }
}
