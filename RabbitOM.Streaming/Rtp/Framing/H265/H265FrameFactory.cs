﻿using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        public void Dispose()
        {
        }

        public void Clear()
        {
        }

        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            foreach ( RtpPacket packet in packets )
            {
                if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) )
                {
					Console.WriteLine( nalUnit );
                }
            }

            return false;
        }
    }
}