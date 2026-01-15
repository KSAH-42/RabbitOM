using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp.H264.Nals
{
    using RabbitOM.Streaming.IO;

    /// <summary>
    /// Represent a H264 stream writer
    /// </summary>
    public sealed class H264StreamWriter : IDisposable
    {
        private readonly H264StreamWriterSettings _settings = new H264StreamWriterSettings();
        
        private readonly MemoryStreamWriter _streamOfNalUnits = new MemoryStreamWriter();
        
        private readonly MemoryStreamWriter _streamOfNalUnitsFragmented = new MemoryStreamWriter();

        private readonly MemoryStreamWriter _output = new MemoryStreamWriter();






        /// <summary>
        /// Gets the settings
        /// </summary>
        public H264StreamWriterSettings Settings
        {
            get => _settings;
        }

        /// <summary>
        /// Gets the length
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
        /// Generate a buffer
        /// </summary>
        /// <returns>returns an array</returns>
        public byte[] ToArray()
        {
            _output.SetLength( 0 );

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
        /// Set the length
        /// </summary>
        /// <param name="value">the new length</param>
        public void SetLength( int value )
        {
            _streamOfNalUnits.SetLength( value );
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="packet">the packet</param>
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
        /// Write the sps
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void WriteSPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H264NalUnit.TryParse( packet.Payload , out H264NalUnit nalUnit ) )
            {
                _settings.SPS = packet.Payload.ToArray();
            }
        }

        /// <summary>
        /// Write pps
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void WritePPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H264NalUnit.TryParse( packet.Payload , out H264NalUnit nalUnit ) )
            {
                _settings.PPS = packet.Payload.ToArray();
            }
        }

        /// <summary>
        /// Write a STAP A
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void WriteStapA( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var aggregate in H264NalUnit.ParseAggregates( packet.Payload ) )
            {
                _streamOfNalUnits.Write( RtpStartCodePrefix.Default );
                _streamOfNalUnits.Write( aggregate );
            }
        }

        /// <summary>
        /// Write a FU A
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <exception cref="ArgumentNullException"/>
        public void WriteFuA( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H264NalUnitFragment.TryParse( packet.Payload , out var nalUnit ) )
            {
                if ( H264NalUnitFragment.IsStartPacket( nalUnit ) )
                {
                    Debug.Assert( _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Clear();
                    _streamOfNalUnitsFragmented.Write( RtpStartCodePrefix.Default );
                    _streamOfNalUnitsFragmented.WriteByte( H264NalUnitFragment.ParseHeader( packet.Payload ) );
                    _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
                }
                else if ( H264NalUnitFragment.IsDataPacket( nalUnit ) )
                {
                    Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
                }
                else if ( H264NalUnitFragment.IsStopPacket( nalUnit ) )
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
