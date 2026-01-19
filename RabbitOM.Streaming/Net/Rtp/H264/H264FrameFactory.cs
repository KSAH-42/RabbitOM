using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    using RabbitOM.Streaming.Net.Rtp.H264.Packets;

    internal sealed class H264FrameFactory : IDisposable
    {
        private readonly H264StreamWriter _writer = new H264StreamWriter();

        public void Configure( H264FrameBuilderConfiguration configuration )
        {
            if ( configuration == null )
            {
                throw new ArgumentNullException( nameof( configuration ) );
            }

            _writer.Settings.SPS = configuration.SPS;
            _writer.Settings.PPS = configuration.PPS;
        }

        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out H264FrameMediaElement result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.Clear();
            
            foreach ( var packet in packets )
            {
                if ( H264Packet.TryParse( packet.Payload , out var format ) )
                {
                    switch ( format.Type )
                    {             
                        case H264PacketType.SINGLE_SPS: 
                            _writer.WriteSPS( packet ); 
                            break;

                        case H264PacketType.SINGLE_PPS: 
                            _writer.WritePPS( packet );
                            break;

                        case H264PacketType.AGGREGATION_STAP_A: 
                            _writer.WriteStapA( packet ); 
                            break;

                        case H264PacketType.FRAGMENTATION_FU_A: 
                            _writer.WriteFuA( packet ); 
                            break;

                        default:

                            if ( format.Type >= H264PacketType.SINGLE_SLICE && format.Type <= H264PacketType.SINGLE_RESERVED_K )
                            {
                                _writer.Write( packet );
                            }

                            break;
                    }
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.TryValidate() )
            {
                result = new H264FrameMediaElement( _writer.ToArray() 
                    , RtpStartCodePrefix.Default 
                    , _writer.Settings.SPS 
                    , _writer.Settings.PPS );
            }

            return result != null;
        }

        public void Clear()
        {
            _writer.Clear();
            _writer.Settings.Clear();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
