﻿using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265FrameReassembler _reassembler = new H265FrameReassembler();

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

            _reassembler.Clear();

            foreach ( RtpPacket packet in packets )
            {
                if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) )
                {
                    _reassembler.AddNalUnit( nalUnit );
                }
            }

            throw new NotImplementedException();
        }
    }
}
