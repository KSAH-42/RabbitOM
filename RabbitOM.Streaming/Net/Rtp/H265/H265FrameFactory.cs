using RabbitOM.Streaming.Net.Rtp.H265.Headers;
using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265FrameBuilderConfiguration _configuration;

        private readonly H265StreamWriter _writer;




        public H265FrameFactory( H265FrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
            _writer = new H265StreamWriter();
        } 





        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.SetLength( 0 );
            
            H265StreamWriterSettings.AssignParameters( _writer.Settings , _configuration.PPS , _configuration.SPS , _configuration.VPS );
     
            foreach ( var packet in packets )
            {
                if ( NalUnitHeader.TryParse( packet.Payload , out var header ) )
                {
                    if ( NalUnitHeader.IsInvalidOrUnDefined( ref header ) )
                    {
                        continue;
                    }

                    switch ( header.Type )
                    {
                        case NatUnitType.PPS: 
                            _writer.WritePPS( packet ); 
                            break;

                        case NatUnitType.SPS: 
                            _writer.WriteSPS( packet ); 
                            break;

                        case NatUnitType.VPS: 
                            _writer.WriteVPS( packet ); 
                            break;

                        case NatUnitType.AGGREGATION: 
                            _writer.WriteAggregation( packet ); 
                            break;

                        case NatUnitType.FRAGMENTATION: 
                            _writer.WriteFragmentation( packet ); 
                            break;

                        default:
                            _writer.Write( packet );
                            break;
                    }
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.HasParameters() )
            {
                result = new H265Frame( _writer.ToArray() , _writer.Settings.PPS , _writer.Settings.SPS , _writer.Settings.VPS , _writer.Settings.GetParamsBuffer() );
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
