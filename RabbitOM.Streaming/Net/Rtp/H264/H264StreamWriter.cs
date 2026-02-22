using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    using RabbitOM.Streaming.IO;
    using RabbitOM.Streaming.Net.Rtp.H264.Payloads;
    using RabbitOM.Streaming.Net.Rtp.H264.Payloads.Types;

    internal sealed class H264StreamWriter : IDisposable
    {
        private readonly H264StreamWriterSettings _settings = new H264StreamWriterSettings();
        private readonly MemoryStreamWriter _streamOfNalUnits = new MemoryStreamWriter();
        private readonly MemoryStreamWriter _streamOfNalUnitsFragmented = new MemoryStreamWriter();
        private readonly MemoryStreamWriter _output = new MemoryStreamWriter();
        private bool _skipFragmentedNals;





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
            _skipFragmentedNals = false;
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

        public void WriteSTAP_A( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var unit in H264PayloadStapA.Parse( packet.Payload ).NalUnits )
            {
                if ( ! H264NalUnit.IsNullOrForbidden( unit ) )
                {
                    _streamOfNalUnits.Write( RtpStartCodePrefix.Default );
                    _streamOfNalUnits.Write( unit );
                }
            }
        }

        public void WriteFU_A( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H264NalUnitFragmentA.TryParse( packet.Payload , out var nalUnit )  )
            {
                _skipFragmentedNals |= nalUnit.ForbiddenBit;

                if ( H264NalUnitFragmentA.IsStartPacket( nalUnit ) )
                {
                    OnWriteStartFU_A( packet , nalUnit );
                    return;
                }

                if ( H264NalUnitFragmentA.IsDataPacket( nalUnit ) )
                {
                    OnWriteDataFU_A( packet , nalUnit );
                    return;
                }

                if ( H264NalUnitFragmentA.IsStopPacket( nalUnit ) )
                {
                    OnWriteStopFU_A( packet , nalUnit );
                    return;
                }
            }
            
            _skipFragmentedNals = true;
        }
        




        private void OnWriteStartFU_A( RtpPacket packet , in H264NalUnitFragmentA nalUnit )
        {
            Debug.Assert( _streamOfNalUnitsFragmented.IsEmpty );

            if ( ! _skipFragmentedNals )
            {
                _streamOfNalUnitsFragmented.Clear();
                _streamOfNalUnitsFragmented.Write( RtpStartCodePrefix.Default );
                _streamOfNalUnitsFragmented.WriteByte( H264NalUnitFragmentA.ReConstructHeader( packet.Payload ) );
                _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
            }
        }

        private void OnWriteDataFU_A( RtpPacket packet , in H264NalUnitFragmentA nalUnit )
        {
            Debug.Assert( ! _streamOfNalUnitsFragmented.IsEmpty );

            if ( ! _skipFragmentedNals )
            {
                _streamOfNalUnitsFragmented.Write( nalUnit.Payload );
            }
        }

        private void OnWriteStopFU_A( RtpPacket packet , in H264NalUnitFragmentA nalUnit )
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
