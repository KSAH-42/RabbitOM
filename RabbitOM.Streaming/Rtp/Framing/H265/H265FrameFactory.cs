using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    // The following implementation is subject to change or to be removed entirely

    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265FrameBuilder _builder;

        private readonly H265StreamBuilder _streamBuilder;

        private readonly H265PacketConverter _converter;







        public H265FrameFactory( H265FrameBuilder builder )
		{
            _builder       = builder ?? throw new ArgumentNullException( nameof( builder ) ); ;

            _streamBuilder = new H265StreamBuilder();
            _converter     = new H265PacketConverter();
        }






        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _streamBuilder.Clear();

            _streamBuilder.VPS = _builder.Configuration.VPS;
            _streamBuilder.SPS = _builder.Configuration.SPS;
            _streamBuilder.PPS = _builder.Configuration.PPS;

            foreach ( RtpPacket packet in packets )
            {
                HandlePacket( packet );
            }

            if ( _streamBuilder.CanBuild() )
            {
                result = new H265Frame( _streamBuilder.Build() , _streamBuilder.VPS , _streamBuilder.SPS , _streamBuilder.PPS );
            }

            return result != null;
        }

        public void Clear()
        {
            _streamBuilder.Clear();
        }

        public void Dispose()
        {
            _streamBuilder.Dispose();
        }







        private void HandlePacket( RtpPacket packet )
        {
            if ( ! _converter.TryConvert( packet , out H265NalUnit nalUnit ) )
            {
                OnError( packet );
                return;
            }

            switch ( nalUnit.Type )
            {
                case NalUnitType.UNDEFINED:
                case NalUnitType.INVALID:
                    OnError( packet );
                    break;

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

                default:
                    OnHandle( packet , nalUnit );
                    break;
            }
        }







        private void OnHandle( RtpPacket packet , H265NalUnit nalUnit )
        {
            _streamBuilder.Write( packet.Payload );
        }

        private void OnHandleVPS( RtpPacket packet , H265NalUnit nalUnit )
        {
            _streamBuilder.Write( packet.Payload );
            
            _streamBuilder.VPS = packet.Payload.ToArray();
        }

        private void OnHandleSPS( RtpPacket packet , H265NalUnit nalUnit )
		{
            _streamBuilder.Write( packet.Payload );

            _streamBuilder.SPS = packet.Payload.ToArray();
        }

        private void OnHandlePPS( RtpPacket packet , H265NalUnit nalUnit )
        {
            _streamBuilder.Write( packet.Payload );

            _streamBuilder.PPS = packet.Payload.ToArray();
        }

        private void OnHandleAggregation( RtpPacket packet , H265NalUnit nalUnit )
        {
            foreach ( var smallNalUnit in nalUnit.GetAggregationUnits() )
            {
                _streamBuilder.Write( smallNalUnit );
            }
        }

        private void OnHandleFragmentation( RtpPacket packet , H265NalUnit nalUnit )
        {
            if ( ! FragmentationUnit.TryParse( nalUnit.Payload , out FragmentationUnit fragmentationUnit ) )
            {
                OnError( packet );
                return;
            }

            if ( fragmentationUnit.StartBit && fragmentationUnit.EndBit )
            {
                OnError( packet );
                return;
            }

            throw new NotImplementedException();
            /*
            if ( fragmentationUnit.StartBit )
            {
            }
            else
            {
            }
            */
        }

        private void OnError( RtpPacket packet )
        {
            _streamBuilder.SetErrorStatus( true );
        }
    }
}
