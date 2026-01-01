using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp.H264.Nals
{
    /// <summary>
    /// Represent a H264 stream writer
    /// </summary>
    public sealed class H264StreamWriter : IDisposable
    {
        internal static readonly byte[] StartCodePrefix = { 0x00 , 0x00 , 0x00 , 0x01 };





        private readonly H264StreamWriterSettings _settings = new H264StreamWriterSettings();
        
        private readonly RtpMemoryStream _streamOfNalUnits = new RtpMemoryStream();
        
        private readonly RtpMemoryStream _streamOfNalUnitsFragmented = new RtpMemoryStream();








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
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _streamOfNalUnits.Dispose();
            _streamOfNalUnitsFragmented.Dispose();
        }

        /// <summary>
        /// Generate a buffer
        /// </summary>
        /// <returns>returns an array</returns>
        public byte[] ToArray()
        {
            return _streamOfNalUnits.ToArray();
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

            _streamOfNalUnits.WriteAsBinary( StartCodePrefix );
            _streamOfNalUnits.WriteAsBinary( packet.Payload );
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
                _streamOfNalUnits.WriteAsBinary( StartCodePrefix );
                _streamOfNalUnits.WriteAsBinary( aggregate );
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
                if ( H264NalUnitFragment.IsStartPacket( ref nalUnit ) )
                {
                    Debug.Assert( _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Clear();
                    _streamOfNalUnitsFragmented.WriteAsBinary( StartCodePrefix );
                    _streamOfNalUnitsFragmented.WriteAsByte( H264NalUnitFragment.ParseHeader( packet.Payload ) );
                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );
                }
                else if ( H264NalUnitFragment.IsDataPacket( ref nalUnit ) )
                {
                    Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );
                }
                else if ( H264NalUnitFragment.IsStopPacket( ref nalUnit ) )
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
