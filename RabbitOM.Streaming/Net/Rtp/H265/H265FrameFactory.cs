using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    using RabbitOM.Streaming.Net.Rtp.H265.Nals;

    /// <summary>
    /// Represente the H265 frame factory
    /// </summary>
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265StreamWriter _writer = new H265StreamWriter();

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="settings">the settings</param>
        public void Configure( H265Settings settings )
        {
            if ( settings == null )
            {
                throw new ArgumentNullException( nameof( settings ) );
            }

            _writer.Settings.PPS  = settings.PPS;
            _writer.Settings.SPS  = settings.SPS;
            _writer.Settings.VPS  = settings.VPS;
            _writer.Settings.DONL = settings.DONL;
        }

        /// <summary>
        /// Try to create the frame
        /// </summary>
        /// <param name="packets">the aggregated packets collection</param>
        /// <param name="result">the output frame</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out H265FrameMediaElement result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.SetLength( 0 );

            foreach ( var packet in packets )
            {
                if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnitType type ) )
                {
                    switch ( type )
                    {
                        case H265NalUnitType.PPS: 
                            _writer.WritePPS( packet ); 
                            break;

                        case H265NalUnitType.SPS: 
                            _writer.WriteSPS( packet ); 
                            break;

                        case H265NalUnitType.VPS: 
                            _writer.WriteVPS( packet ); 
                            break;

                        case H265NalUnitType.AGGREGATION: 
                            _writer.WriteAggregation( packet ); 
                            break;

                        case H265NalUnitType.FRAGMENTATION: 
                            _writer.WriteFragmentation( packet ); 
                            break;

                        default:

                            if ( type != H265NalUnitType.UNKNOWN )
                            {
                                _writer.Write( packet );
                            }

                            break;
                    }
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.TryValidate() )
            {
                result = new H265FrameMediaElement( H265StreamWriter.StartCodePrefix , _writer.Settings.PPS , _writer.Settings.SPS , _writer.Settings.VPS , _writer.ToArray() );
            }

            return result != null;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _writer.Clear();
            _writer.Settings.Clear();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
