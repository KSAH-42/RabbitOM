using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265StreamWriter _writer = new H265StreamWriter();
        
        private readonly H265PacketConverter _converter = new H265PacketConverter();





        // TODO: when the implementation will be finish, handle the case where an exception can be throw


        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.Clear();

            foreach ( RtpPacket packet in packets )
            {
                HandlePacket( packet );
            }

#if ! DEBUG
            throw new NotImplementedException();
#endif

            if ( _writer.Length > 0 )
            {
                result = new RtpFrame( _writer.ToArray() );
            }

            return result != null;
        }

        public void Clear()
        {
            _writer.Clear();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }







        private void HandlePacket( RtpPacket packet )
        {
            H265NalUnit nalUnit = _converter.Convert( packet );

            switch ( nalUnit.Type )
            {
                case NalUnitType.AGGREGATION:
                    OnWriteAggregation( packet , nalUnit );
                    break;

                case NalUnitType.FRAGMENTATION:
                    OnWriteFragmentation( packet , nalUnit );
                    break;

                case NalUnitType.PPS:
                    OnWritePPS( packet , nalUnit );
                    break;

                case NalUnitType.SPS:
                    OnWriteSPS( packet , nalUnit );
                    break;

                case NalUnitType.VPS:
                    OnWriteVPS( packet , nalUnit );
                    break;

                case NalUnitType.UNDEFINED:
                case NalUnitType.INVALID:
                    OnWriteError( packet , nalUnit );
                    break;

                default:
                    OnWrite( packet , nalUnit );
                    break;
            }
        }







        private void OnWrite( RtpPacket packet , H265NalUnit nalUnit )
        {
            _writer.Write( packet.Payload );
        }

        private void OnWritePPS( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnWriteSPS( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnWriteVPS( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnWriteAggregation( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnWriteFragmentation( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnWriteError( RtpPacket packet , H265NalUnit nalUnit )
        {
        }
    }
}
