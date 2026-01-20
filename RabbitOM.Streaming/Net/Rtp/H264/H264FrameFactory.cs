using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    using RabbitOM.Streaming.Net.Rtp.H264.Payloads;

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
                if ( H264Payload.TryParse( packet.Payload , out var payload ) )
                {
                    switch ( payload.Type )
                    {             
                        case H264PayloadType.SINGLE_SPS: 
                            _writer.WriteSPS( packet ); 
                            break;

                        case H264PayloadType.SINGLE_PPS: 
                            _writer.WritePPS( packet );
                            break;

                        case H264PayloadType.AGGREGATION_STAP_A: 
                            _writer.WriteSTAP_A( packet ); 
                            break;

                        case H264PayloadType.FRAGMENTATION_FU_A: 
                            _writer.WriteFU_A( packet ); 
                            break;

                        default:

                            if ( payload.Type >= H264PayloadType.SINGLE_SLICE && payload.Type <= H264PayloadType.SINGLE_RESERVED_K )
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
