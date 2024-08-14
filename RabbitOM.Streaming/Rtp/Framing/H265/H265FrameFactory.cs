using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265NalUnitFrameBuilder _reassembler = new H265NalUnitFrameBuilder();

        public void Dispose()
        {
            _reassembler.Dispose();
        }

        public void Clear()
        {
            _reassembler.Clear();
        }

        public bool TryCreateFrames( IEnumerable<RtpPacket> packets , out IEnumerable<RtpFrame> result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            foreach ( RtpPacket packet in packets )
            {
                _reassembler.AddNalUnit( packet.Payload );
            }

            throw new NotImplementedException();
        }
    }
}
