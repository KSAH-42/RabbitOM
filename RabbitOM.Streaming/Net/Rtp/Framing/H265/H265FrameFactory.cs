using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Framing.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265FrameBuilder _builder;

        private readonly H265StreamBuilder _streamBuilder;

        private readonly H265PacketConverter _converter;







        public H265FrameFactory( H265FrameBuilder builder )
        {
            ExceptionHelper.ThrowOnRelease( "the H265 implementation is not yet finished. It can not be used in production." );

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

            _streamBuilder.Clear( false );

            _streamBuilder.Configure( _builder.Configuration.VPS , _builder.Configuration.SPS , _builder.Configuration.PPS );

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







        // TODO: a code refactoring must done here 
        // This code here is not yet finished
        // Because we need to parse the first 2 bytes of
        // the rtp payload to see if it is a nal unit header
        // or a fragment header
        // before parsing the remaining bytes including the two bytes
        // So here we can continue to used to parse the nalunit
        // but we need to test the forbiddenBit and to see if it's equals to true
        // in order to select the right parser  
        // this code is actually incompleted

        // We can used the same code but we need to change
        // the converter code and it's output type and 
        // to introduce H265Packet class base class
        // for a fragment or nal unit
        // and then a cast be done

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
                    OnHandleFragmentation( packet );
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
            foreach ( var nalu in nalUnit.GetAggregationUnits() )
            {
                _streamBuilder.Write( nalu );
            }
        }

        private void OnHandleFragmentation( RtpPacket packet )
        {
            if ( ! FragmentationUnit.TryParse( packet.Payload , out FragmentationUnit fragmentationUnit ) )
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
        }

        private void OnError( RtpPacket packet )
        {
            _streamBuilder.SetErrorStatus( true );
        }
    }
}
