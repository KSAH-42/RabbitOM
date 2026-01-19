using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp.H264.Payloads
{
    using RabbitOM.Streaming.IO;
    using RabbitOM.Streaming.Net.Rtp.H264.Payloads.Entities;

    public sealed class H264StreamWriter : IDisposable
    {
        private readonly H264StreamWriterSettings _settings = new H264StreamWriterSettings();
        private readonly MemoryStreamWriter _streamOfNalUnits = new MemoryStreamWriter();
        private readonly MemoryStreamWriter _streamOfNalUnitsFragmented = new MemoryStreamWriter();
        private readonly MemoryStreamWriter _output = new MemoryStreamWriter();
        private bool _discardFragmentedNals;



        public H264StreamWriterSettings Settings
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
            _discardFragmentedNals = false;
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

            if ( H264NalUnit.TryParse( packet.Payload , out H264NalUnit nalUnit ) && ! nalUnit.ForbiddenBit )
            {
                _streamOfNalUnits.Write( RtpStartCodePrefix.Default );
                _streamOfNalUnits.Write( packet.Payload );
            }
        }

        public void WriteSPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H264NalUnit.TryParse( packet.Payload , out H264NalUnit nalUnit ) && ! nalUnit.ForbiddenBit )
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

            if ( H264NalUnit.TryParse( packet.Payload , out H264NalUnit nalUnit ) && ! nalUnit.ForbiddenBit )
            {
                _settings.PPS = packet.Payload.ToArray();
            }
        }

        public void WriteStapA( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var unit in H264PayloadStapA.Parse( packet.Payload ).NalUnits )
            {
                if ( H264NalUnit.TryParse( unit , out var nalUnit ) && ! nalUnit.ForbiddenBit )
                {
                    _streamOfNalUnits.Write( RtpStartCodePrefix.Default );
                    _streamOfNalUnits.Write( unit );
                }
            }
        }

        public void WriteFuA( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H264NalUnitFragmentA.TryParse( packet.Payload , out var nalUnit ) )
            {
                _discardFragmentedNals |= nalUnit.ForbiddenBit;

                if ( H264NalUnitFragmentA.IsStartPacket( nalUnit ) )
                {
                    Debug.Assert( _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Clear();
                    _streamOfNalUnitsFragmented.Write( RtpStartCodePrefix.Default );
                    _streamOfNalUnitsFragmented.WriteByte( H264NalUnitFragmentA.ReConstructHeader( packet.Payload ) );
                    _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
                }
                else if ( H264NalUnitFragmentA.IsDataPacket( nalUnit ) )
                {
                    Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
                }
                else if ( H264NalUnitFragmentA.IsStopPacket( nalUnit ) )
                {
                    Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

                    if ( ! _discardFragmentedNals )
                    {
                        _streamOfNalUnitsFragmented.Write( nalUnit.Payload );                    
                        _streamOfNalUnits.Write( _streamOfNalUnitsFragmented );
                    }

                    _streamOfNalUnitsFragmented.Clear();
                    _discardFragmentedNals = false;
                }
            }
            else
            {
                _discardFragmentedNals = true;
            }
        }
    }
}
