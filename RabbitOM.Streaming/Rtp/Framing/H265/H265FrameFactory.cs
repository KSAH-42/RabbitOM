using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265FrameBuilder _builder;

        private readonly H265StreamWriter _writer;

        private readonly H265PacketConverter _converter;







		public H265FrameFactory( H265FrameBuilder builder )
		{
            if ( builder == null )
            {
                throw new ArgumentNullException( nameof( builder ) );
            }

            _builder   = builder;
            _writer    = new H265StreamWriter();
            _converter = new H265PacketConverter();
        }






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

                case NalUnitType.UNDEFINED:
                case NalUnitType.INVALID:
                    OnHandleError( packet , nalUnit );
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
