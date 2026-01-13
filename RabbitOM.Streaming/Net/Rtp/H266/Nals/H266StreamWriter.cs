using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp.H266.Nals
{
    using RabbitOM.Streaming.IO;

    public sealed class H266StreamWriter : IDisposable
    {
        private readonly H266StreamWriterSettings _settings = new H266StreamWriterSettings();
        
        private readonly MemoryStreamBuffer _streamOfNalUnits = new MemoryStreamBuffer();
        
        private readonly MemoryStreamBuffer _streamOfNalUnitsFragmented = new MemoryStreamBuffer();

        private readonly MemoryStreamBuffer _output = new MemoryStreamBuffer();

        private byte[] _rawVPS;

        private byte[] _rawSPS;

        private byte[] _rawPPS;



        
        public H266StreamWriterSettings Settings
        {
            get => _settings;
        }

        public long Length
        {
            get => _streamOfNalUnits.Length;
        }


        


        public void Clear()
        {
            _streamOfNalUnits.Clear();
            _streamOfNalUnitsFragmented.Clear();
            
            _output.Clear();
        }

        public void ClearParameters()
        {
            _rawVPS = null;
            _rawSPS = null;
            _rawPPS = null;
        }

        public void Dispose()
        {
            _streamOfNalUnits.Dispose();
            _streamOfNalUnitsFragmented.Dispose();

            _output.Dispose();
        }

        public byte[] ToArray()
        {
            _output.SetLength( 0 );

            if ( _rawVPS?.Length > 0 )
            {
                _output.Write( StartCodePrefix.Default );
                _output.Write( _rawVPS );
            }

            if ( _rawSPS?.Length > 0 )
            {
                _output.Write( StartCodePrefix.Default );
                _output.Write( _rawSPS );
            }

            if ( _rawPPS?.Length > 0 )
            {
                _output.Write( StartCodePrefix.Default );
                _output.Write( _rawPPS );
            }

            _output.Write( _streamOfNalUnits );

            return _output.ToArray();
        }
        
        public void SetLength( int value )
        {
            _streamOfNalUnits.SetLength( value );
        }

        public void Write( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _streamOfNalUnits.Write( StartCodePrefix.Default );
            _streamOfNalUnits.Write( packet.Payload );
        }

        
        public void WriteVPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H266NalUnit.TryParse( packet.Payload , out H266NalUnit nalUnit ) )
            {
                _rawVPS = packet.Payload.ToArray();
                _settings.VPS = nalUnit.Payload.ToArray();
            }
        }

        public void WriteSPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H266NalUnit.TryParse( packet.Payload , out H266NalUnit nalUnit ) )
            {
                _rawSPS = packet.Payload.ToArray();
                _settings.SPS = nalUnit.Payload.ToArray();
            }
        }

        public void WritePPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H266NalUnit.TryParse( packet.Payload , out H266NalUnit nalUnit ) )
            {
                _rawPPS = packet.Payload.ToArray();
                _settings.PPS = nalUnit.Payload.ToArray();
            }
        }

        public void WriteAggregation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var aggregate in H266NalUnit.ParseAggregates( packet.Payload ) )
            {
                _streamOfNalUnits.Write( StartCodePrefix.Default );
                _streamOfNalUnits.Write( aggregate );
            }
        }

        public void WriteFragmentation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H266NalUnitFragment.TryParse( packet.Payload ,_settings.DONL , out var nalUnit ) )
            {
                if ( H266NalUnitFragment.IsStartPacket( nalUnit ) )
                {
                    Debug.Assert( _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Clear();
                    _streamOfNalUnitsFragmented.Write( StartCodePrefix.Default );
                    _streamOfNalUnitsFragmented.WriteUInt16( H266NalUnitFragment.ParseHeader( packet.Payload ) );
                    _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
                }
                else if ( H266NalUnitFragment.IsDataPacket( nalUnit ) )
                {
                    Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
                }
                else if ( H266NalUnitFragment.IsStopPacket( nalUnit ) )
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
