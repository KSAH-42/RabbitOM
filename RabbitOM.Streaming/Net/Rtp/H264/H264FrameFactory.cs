using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    using RabbitOM.Streaming.Net.Rtp.H264.Nals;

    /// <summary>
    /// Represent the H264 frame factory
    /// </summary>
    internal sealed class H264FrameFactory : IDisposable
    {
        private readonly H264StreamWriter _writer = new H264StreamWriter();

        /// <summary>
        /// Setup
        /// </summary>
        /// <param name="settings">the settings</param>
        public void Configure( H264FrameBuilderConfiguration settings )
        {
            if ( settings == null )
            {
                throw new ArgumentNullException( nameof( settings ) );
            }

            _writer.Settings.SPS = settings.SPS;
            _writer.Settings.PPS = settings.PPS;
        }

        /// <summary>
        /// Try to create the frame
        /// </summary>
        /// <param name="packets">the packets</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out H264FrameMediaElement result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.SetLength( 0 );
            
            foreach ( var packet in packets )
            {
                if ( H264NalUnit.TryParse( packet.Payload , out H264NalUnitType type ) )
                {
                    switch ( type )
                    {             
                        case H264NalUnitType.SINGLE_SPS: 
                            _writer.WriteSPS( packet ); 
                            break;

                        case H264NalUnitType.SINGLE_PPS: 
                            _writer.WritePPS( packet ); 
                            break;

                        case H264NalUnitType.AGGREGATION_STAP_A: 
                            _writer.WriteStapA( packet ); 
                            break;

                        case H264NalUnitType.FRAGMENTATION_FU_A: 
                            _writer.WriteFuA( packet ); 
                            break;

                        default:

                            if ( type >= H264NalUnitType.SINGLE_SLICE && type <= H264NalUnitType.SINGLE_RESERVED_K )
                            {
                                _writer.Write( packet );
                            }

                            break;
                    }
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.TryValidate() )
            {
                result = new H264FrameMediaElement( StartCodePrefix.Default , _writer.Settings.SPS , _writer.Settings.PPS , _writer.ToArray() );
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
