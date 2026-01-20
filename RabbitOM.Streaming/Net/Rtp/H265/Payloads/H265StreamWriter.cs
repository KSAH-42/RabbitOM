using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp.H265.Payloads
{
    using RabbitOM.Streaming.IO;
    using RabbitOM.Streaming.Net.Rtp.H265.Payloads.Entities;

    /// <summary>
    /// Represent the H265 stream writer used to generate a H265 data frame
    /// </summary>
    public sealed class H265StreamWriter : IDisposable
    {
        private readonly H265StreamWriterSettings _settings = new H265StreamWriterSettings();
        private readonly MemoryStreamWriter _streamOfNalUnits = new MemoryStreamWriter();
        private readonly MemoryStreamWriter _streamOfNalUnitsFragmented = new MemoryStreamWriter();
        private readonly MemoryStreamWriter _output = new MemoryStreamWriter();
        private bool _skipFragmentedNals;
        






        
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
            _skipFragmentedNals = false;
            _streamOfNalUnits.Clear();
            _streamOfNalUnitsFragmented.Clear();
            _output.Clear();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _streamOfNalUnits.Dispose();
            _streamOfNalUnitsFragmented.Dispose();

            _output.Dispose();
        }

        /// <summary>
        /// Generate the data frame
        /// </summary>
        /// <returns>returns a byte array</returns>
        public byte[] ToArray()
        {
            _output.SetLength( 0 );

            if ( _settings.VPS?.Length > 0 )
            {
                _output.Write( RtpStartCodePrefix.Default );
                _output.Write( _settings.VPS );
            }

            if ( _settings.SPS?.Length > 0 )
            {
                _output.Write( RtpStartCodePrefix.Default );
                _output.Write( _settings.SPS );
            }

            if ( _settings.PPS?.Length > 0 )
            {
                _output.Write( RtpStartCodePrefix.Default );
                _output.Write( _settings.PPS );
            }

            _output.Write( _streamOfNalUnits );

            return _output.ToArray();
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

            _streamOfNalUnits.Write( RtpStartCodePrefix.Default );
            _streamOfNalUnits.Write( packet.Payload );
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

            if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) && ! nalUnit.ForbiddenBit )
            {
                _settings.VPS = packet.Payload.ToArray();
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

            if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) && ! nalUnit.ForbiddenBit )
            {
                _settings.SPS = packet.Payload.ToArray();
            }
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

            if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) && ! nalUnit.ForbiddenBit )
            {
                _settings.PPS = packet.Payload.ToArray();
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

            foreach ( var nalUnit in H265PayloadAggregation.Parse( packet.Payload , _settings.DONL ).NalUnits )
            {
                if ( ! H265NalUnit.IsNullOrForbidden( nalUnit ) )
                {
                    _streamOfNalUnits.Write( RtpStartCodePrefix.Default );
                    _streamOfNalUnits.Write( nalUnit );
                }
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

            if ( H265NalUnitFragment.TryParse( packet.Payload , Settings.DONL ,  out var nalUnit ) )
            {
                _skipFragmentedNals |= nalUnit.ForbiddenBit;

                if ( H265NalUnitFragment.IsStartPacket( nalUnit ) )
                {
                    OnWriteFragmentationStart( packet , nalUnit );
                    return;
                }
                
                if ( H265NalUnitFragment.IsDataPacket( nalUnit ) )
                {
                    OnWriteFragmentationData( packet , nalUnit );
                    return;
                }
                
                if ( H265NalUnitFragment.IsStopPacket( nalUnit ) )
                {
                    OnWriteFragmentationStop( packet , nalUnit);
                    return;
                }
            }

            _skipFragmentedNals = true;
        }
        




        private void OnWriteFragmentationStart( RtpPacket packet , in H265NalUnitFragment nalUnit )
        {
            Debug.Assert( _streamOfNalUnitsFragmented.IsEmpty );

            if ( ! _skipFragmentedNals )
            {
                _streamOfNalUnitsFragmented.Clear();
                _streamOfNalUnitsFragmented.Write( RtpStartCodePrefix.Default );
                _streamOfNalUnitsFragmented.WriteUInt16( H265NalUnitFragment.ReConstructHeader( packet.Payload ) );
                _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
            }
        }

        private void OnWriteFragmentationData( RtpPacket packet , in H265NalUnitFragment nalUnit )
        {
            Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

            if ( ! _skipFragmentedNals )
            {
                _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
            }
        }

        private void OnWriteFragmentationStop( RtpPacket packet , in H265NalUnitFragment nalUnit )
        {
            Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

            if ( ! _skipFragmentedNals )
            {
                _streamOfNalUnitsFragmented.Write( nalUnit.Payload );                    
                _streamOfNalUnits.Write( _streamOfNalUnitsFragmented );
            }

            _streamOfNalUnitsFragmented.Clear();
            _skipFragmentedNals = false;
        }
    }
}
