using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCFrameFactory : IDisposable
    {
        private readonly HEVCFrameBuilderConfiguration _configuration;
        private readonly HEVCStreamWriter _writer;

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

            OnPrepare();
     
            foreach ( var packet in packets )
            {
                throw new NotImplementedException();
            }

            if ( _writer.Length > 0 && _writer.HasParametersSets() )
            {
                result = new HEVCFrame( _writer.ToArray() );
            }

            return result != null;
        }

        public void Clear()
        {
            _writer.Clear();
            _writer.ClearParameters();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }






        private void OnPrepare()
        {
            _writer.Clear();

            _writer.PPS = _writer.PPS?.Length > 0 ? _writer.PPS : _configuration.PPS;
            _writer.SPS = _writer.SPS?.Length > 0 ? _writer.SPS : _configuration.SPS;
            _writer.VPS = _writer.VPS?.Length > 0 ? _writer.VPS : _configuration.VPS;
        }

        private void OnWritePacket( HEVCPacket packet )
        {
            switch ( packet.HeaderType )
            {
                case HEVCPacketType.PPS: 
                    _writer.WritePPS( packet ); 
                    break;

                case HEVCPacketType.SPS: 
                    _writer.WriteSPS( packet ); 
                    break;

                case HEVCPacketType.VPS: 
                    _writer.WriteVPS( packet ); 
                    break;

                case HEVCPacketType.AGGREGATION: 
                    _writer.WriteAU( packet ); 
                    break;

                case HEVCPacketType.FRAGMENTATION: 
                    _writer.WriteFU( packet ); 
                    break;

                case HEVCPacketType.INVALID:
                case HEVCPacketType.UNDEFINED:
                    break;
               
                default:
                    _writer.Write( packet );
                    break;
            }
        }
    }
}
