using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent the H265 stream writer used to generate a H265 data frame
    /// </summary>
    public sealed class H265StreamWriter : IDisposable
    {
        private readonly H265StreamWriterSettings _settings = new H265StreamWriterSettings();
        
        private readonly RtpMemoryStream _streamOfNalUnits = new RtpMemoryStream();
        
        private readonly RtpMemoryStream _streamOfNalUnitsFragmented = new RtpMemoryStream();

        private readonly RtpMemoryStream _streamOfNalUnitsParams = new RtpMemoryStream();

        private readonly RtpMemoryStream _output = new RtpMemoryStream();








        
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

            _output.WriteAsBinary( _streamOfNalUnitsParams );
            _output.WriteAsBinary( _streamOfNalUnits );

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

            _streamOfNalUnits.WriteAsBinary( _settings.StartCodePrefix );
            _streamOfNalUnits.WriteAsBinary( packet.Payload );
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

            if ( H265NalUnit.TryParse( packet.Payload , out var nalUnit ) )
            {
                _streamOfNalUnitsParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnitsParams.WriteAsBinary( packet.Payload );

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

            if ( H265NalUnit.TryParse( packet.Payload , out var nalUnit ) )
            {
                _streamOfNalUnitsParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnitsParams.WriteAsBinary( packet.Payload );

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

            if ( H265NalUnit.TryParse( packet.Payload , out var nalUnit ) )
            {
                _streamOfNalUnitsParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnitsParams.WriteAsBinary( packet.Payload );

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
                _streamOfNalUnits.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnits.WriteAsBinary( aggregate );
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
                if ( H265NalUnitFragment.IsStartPacket( ref nalUnit ) )
                {
                    Debug.Assert( _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Clear();
                    _streamOfNalUnitsFragmented.WriteAsBinary( _settings.StartCodePrefix );
                    _streamOfNalUnitsFragmented.WriteAsUInt16( H265NalUnitFragment.ParseHeader( packet.Payload ) );
                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );
                }
                else if ( H265NalUnitFragment.IsDataPacket( ref nalUnit ) )
                {
                    Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );
                }
                else if ( H265NalUnitFragment.IsStopPacket( ref nalUnit ) )
                {
                    Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );                    
                    _streamOfNalUnits.WriteAsBinary( _streamOfNalUnitsFragmented );
                    _streamOfNalUnitsFragmented.Clear();
                }
            }
        }
    }
}
