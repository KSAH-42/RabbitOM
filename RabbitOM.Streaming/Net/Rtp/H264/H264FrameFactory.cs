using RabbitOM.Streaming.Net.Rtp.H264.Headers;
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
            ExceptionHelper.ThrowOnRelease( "DONT use in production - implementation is progress" );

            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
            _writer = new H264StreamWriter();
        } 





        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.SetLength( 0 );
            
            H264StreamWriterSettings.AssignParameters( _writer.Settings , _configuration.PPS , _configuration.SPS );
     
            foreach ( var packet in packets )
            {
                if ( NalUnit.TryParse( packet.Payload , out var nalUnit ) )
                {
                    if ( NalUnit.IsInvalidOrUnDefined( ref nalUnit ) )
                    {
                        continue;
                    }
                    
                    switch ( nalUnit.Type )
                    {
                        case NalUnitType.SINGLE_PPS: 
                            _writer.WritePPS( packet ); 
                            break;

                        case NalUnitType.SINGLE_SPS: 
                            _writer.WriteSPS( packet ); 
                            break;

                        case NalUnitType.AGGREGATION_STAP_A: 
                            _writer.WriteAggregation( packet ); 
                            break;

                        case NalUnitType.FRAGMENTATION_FU_A: 
                            _writer.WriteFragmentation( packet ); 
                            break;

                        default:

                            if ( NalUnit.IsSingle( ref nalUnit ) )
                            {
                                _writer.Write( packet );
                            }

                            break;
                    }
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.TryValidate() )
            {
                result = new H264Frame( _writer.ToArray() , _writer.Settings.PPS , _writer.Settings.SPS , _writer.Settings.BuildParamsBuffer() );
            }

            return result != null;
        }

        public void Clear()
        {
            _writer.Clear();
        }

        public void Dispose()
        {
            _writer.Clear();
            _writer.Dispose();
        }
    }
}
