using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp.H266.Payloads
{
    using RabbitOM.Streaming.IO;
    using RabbitOM.Streaming.Net.Rtp.H266.Payloads.Entities;

    public sealed class H266StreamWriter : IDisposable
    {
        private readonly H266StreamWriterSettings _settings = new H266StreamWriterSettings();
        
        private readonly MemoryStreamWriter _streamOfNalUnits = new MemoryStreamWriter();
        
        private readonly MemoryStreamWriter _streamOfNalUnitsFragmented = new MemoryStreamWriter();

        private readonly MemoryStreamWriter _output = new MemoryStreamWriter();




        
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

        public void Dispose()
        {
            _streamOfNalUnits.Dispose();
            _streamOfNalUnitsFragmented.Dispose();

            _output.Dispose();
        }

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

            _streamOfNalUnits.Write( RtpStartCodePrefix.Default );
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
                _settings.VPS = packet.Payload.ToArray();
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
                _settings.SPS = packet.Payload.ToArray();
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
                _settings.PPS = packet.Payload.ToArray();
            }
        }

        public void WriteAggregation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var aggregate in H266PayloadAggregate.Parse( packet.Payload , _settings.DONL ).NalUnits )
            {
                _streamOfNalUnits.Write( RtpStartCodePrefix.Default );
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
                    _streamOfNalUnitsFragmented.Write( RtpStartCodePrefix.Default );
                    _streamOfNalUnitsFragmented.WriteUInt16( H266NalUnitFragment.ReContructHeader( packet.Payload ) );
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
