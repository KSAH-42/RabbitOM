using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265FrameReassembler _reassembler = new H265FrameReassembler();

        public void Dispose()
        {
        }

        public void Clear()
        {
        }

        public bool TryCreateFrames( IEnumerable<RtpPacket> packets , out IEnumerable<RtpFrame> result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            throw new NotImplementedException();
        }
    }
}
