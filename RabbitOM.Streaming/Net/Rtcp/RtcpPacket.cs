using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public abstract class RtcpPacket
    {
        public const byte DefaultVersion = 2;


        protected RtcpPacket( byte version )
        { 
            ExceptionHelper.ThrowOnRelease("this implementation is not yet finished");

            Version = version; 
        }

        public byte Version { get; private set; }
    }
}
