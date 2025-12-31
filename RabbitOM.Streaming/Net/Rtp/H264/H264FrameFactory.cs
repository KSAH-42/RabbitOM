using RabbitOM.Streaming.Net.Rtp.H264.Nals;
using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public sealed class H264FrameFactory : IDisposable
    {
        private readonly H264FrameBuilderConfiguration _configuration;

        private readonly H264StreamWriter _writer;




        public H264FrameFactory( H264FrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
            _writer = new H264StreamWriter();
        } 



        public void Setup()
        {
            if ( _writer.Settings.PPS == null || _writer.Settings.PPS.Length == 0 )
            {
                _writer.Settings.PPS = _configuration.PPS;
            }

            if ( _writer.Settings.SPS == null || _writer.Settings.SPS.Length == 0 )
            {
                _writer.Settings.SPS = _configuration.SPS;
            }
        }

        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.SetLength( 0 );
            
            foreach ( var packet in packets )
            {
                var type = H264NalUnit.ParseType( packet.Payload );

                switch ( type )
                {             
                    case H264NalUnitType.AGGREGATION_STAP_A: 
                        _writer.WriteStapA( packet ); 
                        break;

                    case H264NalUnitType.FRAGMENTATION_FU_A: 
                        _writer.WriteFuA( packet ); 
                        break;

                    case H264NalUnitType.SINGLE_PPS: 
                        _writer.WritePPS( packet ); 
                        break;

                    case H264NalUnitType.SINGLE_SPS: 
                        _writer.WriteSPS( packet ); 
                        break;

                    default:

                        if ( type >= H264NalUnitType.SINGLE_SLICE && type <= H264NalUnitType.SINGLE_RESERVED_K )
                        {
                            _writer.Write( packet );
                        }

                        break;
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.TryValidate() )
            {
                result = new H264Frame( _writer.ToArray() , H264StreamWriter.StartCodePrefix , _writer.Settings.PPS , _writer.Settings.SPS );
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
