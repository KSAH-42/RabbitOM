using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265FrameBuilder _builder;

        private readonly H265StreamWriter _writer;







		public H265FrameFactory( H265FrameBuilder builder )
		{
            _builder = builder ?? throw new ArgumentNullException( nameof( builder ) ); ;
            _writer  = new H265StreamWriter();
        }






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
                if ( ! H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) )
                {
                    return false;
                }

                if ( ! nalUnit.TryValidate() )
                {
                    return false;
                }

                HandlePacket( packet , nalUnit );
            }

            if ( _writer.Length > 0 )
            {
                // TODO: replace by using the right frame type
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







        private void HandlePacket( RtpPacket packet , H265NalUnit nalUnit )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( nalUnit == null )
            {
                throw new ArgumentNullException( nameof( nalUnit ) );
            }

            switch ( nalUnit.Type )
            {
                case NalUnitType.UNDEFINED:
                case NalUnitType.INVALID:
                    OnHandleError( packet , nalUnit );
                    break;

                case NalUnitType.AGGREGATION:
                    OnHandleAggregation( packet , nalUnit );
                    break;

                case NalUnitType.FRAGMENTATION:
                    OnHandleFragmentation( packet , nalUnit );
                    break;

                case NalUnitType.PPS:
                    OnHandlePPS( packet , nalUnit );
                    break;

                case NalUnitType.SPS:
                    OnHandleSPS( packet , nalUnit );
                    break;

                case NalUnitType.VPS:
                    OnHandleVPS( packet , nalUnit );
                    break;

                default:
                    OnHandle( packet , nalUnit );
                    break;
            }
        }







        private void OnHandle( RtpPacket packet , H265NalUnit nalUnit )
        {
            _writer.Write( packet.Payload );
        }

        private void OnHandlePPS( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnHandleSPS( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnHandleVPS( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnHandleAggregation( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnHandleFragmentation( RtpPacket packet , H265NalUnit nalUnit )
        {
        }

        private void OnHandleError( RtpPacket packet , H265NalUnit nalUnit )
        {
        }
    }
}
