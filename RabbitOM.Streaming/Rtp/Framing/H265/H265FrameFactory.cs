using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265StreamWriter _writer = new H265StreamWriter();
        private readonly H265PacketConverter _converter = new H265PacketConverter();

        public void Dispose()
        {
            _writer.Dispose();
        }

        public void Clear()
        {
            _writer.Clear();
        }

        public bool TryCreateFrames( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.Clear();

            foreach ( RtpPacket packet in packets )
            {
                H265NalUnit nalUnit = _converter.Convert( packet );

                if ( nalUnit.Type == NalUnitType.AGGREGATION )
                {
                    OnAddAggregation( packet , nalUnit );
                }

                else if ( nalUnit.Type == NalUnitType.FRAGMENTATION )
                {
                    OnAddFragmentation( packet , nalUnit );
                }

                else
                {
                    OnAdd( packet , nalUnit );
                }
            }

            throw new NotImplementedException();
        }




        private void OnAdd( RtpPacket packet , H265NalUnit nalu )
        {
            _writer.Write( packet.Payload );
        }

        private void OnAddAggregation( RtpPacket packet , H265NalUnit nalu )
        {
        }

        private void OnAddFragmentation( RtpPacket packet , H265NalUnit nalu )
        {
        }
    }
}
