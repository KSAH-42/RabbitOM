using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    // The following implementation is subject to change or to be removed entirely

    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265FrameBuilder _builder;

        private readonly H265StreamWriter _writer;

        private readonly H265PacketConverter _converter;







        public H265FrameFactory( H265FrameBuilder builder )
		{
            _builder = builder ?? throw new ArgumentNullException( nameof( builder ) ); ;
            _writer  = new H265StreamWriter();
            _converter = new H265PacketConverter();
        }






        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.Clear();

            _writer.Configure( _builder.Configuration.VPS , _builder.Configuration.SPS , _builder.Configuration.PPS );

            foreach ( RtpPacket packet in packets )
            {
                if ( _converter.TryConvert( packet , out H265NalUnit nalunit ) )
                {
                    HandlePacket( packet , nalunit );
                }
                else
                {
                    return false;
                }
            }

            if ( _writer.Length > 0 )
            {
                result = new H265Frame( _writer.ToArray() , _writer.VPS , _writer.SPS , _writer.PPS );
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
            switch ( nalUnit.Type )
            {
                case NalUnitType.AGGREGATION:
                    OnHandleAggregation( packet , nalUnit );
                    break;

                case NalUnitType.FRAGMENTATION:
                    OnHandleFragmentation( packet , nalUnit );
                    break;

                case NalUnitType.VPS:
                    OnHandleVPS( packet , nalUnit );
                    break;

                case NalUnitType.SPS:
                    OnHandleSPS( packet , nalUnit );
                    break;

                case NalUnitType.PPS:
                    OnHandlePPS( packet , nalUnit );
                    break;

                case NalUnitType.UNDEFINED:
                    OnHandleError( packet , nalUnit );
                    break;

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

        private void OnHandleVPS( RtpPacket packet , H265NalUnit nalUnit )
        {
            _writer.WriteVPS( packet.Payload );
        }

        private void OnHandleSPS( RtpPacket packet , H265NalUnit nalUnit )
        {
            _writer.WriteSPS( packet.Payload );
        }

        private void OnHandlePPS( RtpPacket packet , H265NalUnit nalUnit )
        {
            _writer.WritePPS( packet.Payload );
        }

        private void OnHandleAggregation( RtpPacket packet , H265NalUnit nalUnit )
        {
            throw new NotImplementedException();
        }

        private void OnHandleFragmentation( RtpPacket packet , H265NalUnit nalUnit )
        {
            throw new NotImplementedException();
        }

        private void OnHandleError( RtpPacket packet , H265NalUnit nalUnit )
        {
            throw new NotImplementedException();
        }
    }
}
