using System;

namespace RabbitOM.Streaming.Rtcp
{
    public abstract class RtcpPacket
    {
        public const byte DefaultVersion = 2;

        protected RtcpPacket() : this( DefaultVersion )
        {
        }

        protected RtcpPacket( byte version )
        {
#if !DEBUG
            throw new NotImplementedException( "do not used this implementation, because it is not yet finished" );
#endif
            Version = version;
        }

        public byte Version { get; }
    }
}
