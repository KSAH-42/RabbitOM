using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCFrameFactory : IDisposable
    {
        private readonly HEVCFrameBuilderConfiguration _configuration;
        private readonly HEVCStreamWriter _writer = new HEVCStreamWriter();

        public HEVCFrameFactory( HEVCFrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
            _writer = new HEVCStreamWriter();
        } 

        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.Clear();

            foreach ( var packet in packets )
            {
                if ( HEVCPacket.TryParse( packet.Payload , out var naluPacket ) && naluPacket.TryValidate() )
                {
                    OnWritePacket( naluPacket );
                }
            }

            if ( _writer.Length > 0 && _writer.HasParametersSets() )
            {
                result = new HEVCFrame( _writer.ToArray() , _writer.PPS , _writer.SPS , _writer.VPS );
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






        private void OnWritePacket( HEVCPacket packet )
        {
            switch ( packet.HeaderType )
            {
                case HEVCPacketType.PPS: _writer.WritePPS( packet ); break;
                case HEVCPacketType.SPS: _writer.WriteSPS( packet ); break;
                case HEVCPacketType.VPS: _writer.WriteVPS( packet ); break;
                case HEVCPacketType.AGGREGATION: _writer.WriteAU( packet ); break;
                case HEVCPacketType.FRAGMENTATION: _writer.WriteFU( packet ); break;

                case HEVCPacketType.INVALID:
                case HEVCPacketType.UNDEFINED:break;
               
                default:
                    _writer.Write( packet );
                    break;
            }
        }
    }
}
