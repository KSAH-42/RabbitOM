using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public abstract class RtcpPacket
    {
        public const byte DefaultVersion = 2;

        protected RtcpPacket() : this( DefaultVersion )
        { 
        }

        protected RtcpPacket( byte version )
        { 
            ExceptionHelper.ThrowOnRelease("do not used this implementation, because it is not yet finished");

            Version = version; 
        }

        public byte Version { get; private set; }
    }
}
