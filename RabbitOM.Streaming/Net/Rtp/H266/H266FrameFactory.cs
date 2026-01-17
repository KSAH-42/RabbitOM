using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H266
{
    using RabbitOM.Streaming.Net.Rtp.H266.Nals;

    internal sealed class H266FrameFactory : IDisposable
    {
        private readonly H266StreamWriter _writer = new H266StreamWriter();

        public void Configure( H266FrameBuilderConfiguration configuration )
        {
            if ( configuration == null )
            {
                throw new ArgumentNullException( nameof( configuration ) );
            }

            _writer.Settings.PPS  = configuration.PPS;
            _writer.Settings.SPS  = configuration.SPS;
            _writer.Settings.VPS  = configuration.VPS;
            _writer.Settings.DONL = configuration.DONL;
        }

        public bool TryCreate( IEnumerable<RtpPacket> packets , out H266FrameMediaElement result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.Clear();

            foreach ( var packet in packets )
            {
                if ( H266NalUnit.TryParse( packet.Payload , out H266NalUnitType type ) )
                {
                    switch ( type )
                    {
                        case H266NalUnitType.PPS: 
                            _writer.WritePPS( packet ); 
                            break;

                        case H266NalUnitType.SPS: 
                            _writer.WriteSPS( packet ); 
                            break;

                        case H266NalUnitType.VPS: 
                            _writer.WriteVPS( packet ); 
                            break;

                        case H266NalUnitType.RSVNVCL_28: 
                            _writer.WriteAggregation( packet ); 
                            break;

                        case H266NalUnitType.RSVNVCL_29: 
                            _writer.WriteFragmentation( packet ); 
                            break;

                        default:

                            if ( type != H266NalUnitType.UNKNOWN )
                            {
                                _writer.Write( packet );
                            }

                            break;
                    }
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.TryValidate() )
            {
                result = new H266FrameMediaElement( _writer.ToArray() 
                    , RtpStartCodePrefix.Default 
                    , _writer.Settings.PPS 
                    , _writer.Settings.SPS 
                    , _writer.Settings.VPS );
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
