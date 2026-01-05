using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp.H265.Nals
{
    using RabbitOM.Streaming.IO;

    /// <summary>
    /// Represent the H265 stream writer used to generate a H265 data frame
    /// </summary>
    public sealed class H265StreamWriter : IDisposable
    {
        internal static readonly byte[] StartCodePrefix = { 0x00 , 0x00 , 0x00 , 0x01 };






        private readonly H265StreamWriterSettings _settings = new H265StreamWriterSettings();
        
        private readonly MemoryStreamBuffer _streamOfNalUnits = new MemoryStreamBuffer();
        
        private readonly MemoryStreamBuffer _streamOfNalUnitsFragmented = new MemoryStreamBuffer();

        private readonly MemoryStreamBuffer _streamOfNalUnitsParams = new MemoryStreamBuffer();

        private readonly MemoryStreamBuffer _output = new MemoryStreamBuffer();








        
        /// <summary>
        /// Gets the settings of writer
        /// </summary>
        public H265StreamWriterSettings Settings
        {
            get => _settings;
        }

        /// <summary>
        /// Gets the Length
        /// </summary>
        public long Length
        {
            get => _streamOfNalUnits.Length;
        }

        




        


        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _streamOfNalUnits.Clear();
            _streamOfNalUnitsFragmented.Clear();
            _streamOfNalUnitsParams.Clear();
            
            _output.Clear();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _streamOfNalUnits.Dispose();
            _streamOfNalUnitsFragmented.Dispose();
            _streamOfNalUnitsParams.Dispose();

            _output.Dispose();
        }

        /// <summary>
        /// Generate the data frame
        /// </summary>
        /// <returns>returns a byte array</returns>
        public byte[] ToArray()
        {
            _output.SetLength( 0 );

            _output.Write( _streamOfNalUnitsParams );
            _output.Write( _streamOfNalUnits );

            return _output.ToArray();
        }
        
        /// <summary>
        /// Set the length
        /// </summary>
        /// <param name="value">the size</param>
        public void SetLength( int value )
        {
            _streamOfNalUnits.SetLength( value );
        }

        /// <summary>
        /// Write a nalu from the rtp packet
        /// </summary>
        /// <param name="packet">the rtp packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void Write( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _streamOfNalUnits.Write( StartCodePrefix );
            _streamOfNalUnits.Write( packet.Payload );
        }

        /// <summary>
        /// Write a nalu pps from the rtp packet
        /// </summary>
        /// <param name="packet">the rtp packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void WritePPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) )
            {
                _streamOfNalUnitsParams.Write( StartCodePrefix );
                _streamOfNalUnitsParams.Write( packet.Payload );

                _settings.PPS = nalUnit.Payload.ToArray();
            }
        }

        /// <summary>
        /// Write a nalu sps from the rtp packet
        /// </summary>
        /// <param name="packet">the rtp packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void WriteSPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) )
            {
                _streamOfNalUnitsParams.Write( StartCodePrefix );
                _streamOfNalUnitsParams.Write( packet.Payload );

                _settings.SPS = nalUnit.Payload.ToArray();
            }
        }

        /// <summary>
        /// Write a nalu vps from the rtp packet
        /// </summary>
        /// <param name="packet">the rtp packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void WriteVPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) )
            {
                _streamOfNalUnitsParams.Write( StartCodePrefix );
                _streamOfNalUnitsParams.Write( packet.Payload );

                _settings.VPS = nalUnit.Payload.ToArray();
            }
        }

        /// <summary>
        /// Write aggregated nalus from the rtp packet
        /// </summary>
        /// <param name="packet">the rtp packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void WriteAggregation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var aggregate in H265NalUnit.ParseAggregates( packet.Payload ) )
            {
                _streamOfNalUnits.Write( StartCodePrefix );
                _streamOfNalUnits.Write( aggregate );
            }
        }

        /// <summary>
        /// Write a partial / fragmented nalu from the rtp packet
        /// </summary>
        /// <param name="packet">the rtp packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void WriteFragmentation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H265NalUnitFragment.TryParse( packet.Payload , out var nalUnit ) )
            {
                if ( H265NalUnitFragment.IsStartPacket( nalUnit ) )
                {
                    Debug.Assert( _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Clear();
                    _streamOfNalUnitsFragmented.Write( StartCodePrefix );
                    _streamOfNalUnitsFragmented.WriteUInt16( H265NalUnitFragment.ParseHeader( packet.Payload ) );
                    _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
                }
                else if ( H265NalUnitFragment.IsDataPacket( nalUnit ) )
                {
                    Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
                }
                else if ( H265NalUnitFragment.IsStopPacket( nalUnit ) )
                {
                    Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Write( nalUnit.Payload );                    
                    _streamOfNalUnits.Write( _streamOfNalUnitsFragmented );
                    _streamOfNalUnitsFragmented.Clear();
                }
            }
        }
    }
}
