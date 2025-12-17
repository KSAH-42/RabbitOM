using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public sealed class H264StreamWriter : IDisposable
    {
        private readonly H264StreamWriterSettings _settings = new H264StreamWriterSettings();
        
        private readonly RtpMemoryStream _streamOfNalUnits = new RtpMemoryStream();
        
        private readonly RtpMemoryStream _streamOfNalUnitsFragmented = new RtpMemoryStream();

        private readonly RtpMemoryStream _streamOfNalUnitsParams = new RtpMemoryStream();

        private readonly RtpMemoryStream _output = new RtpMemoryStream();








        
        
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
            _streamOfNalUnitsParams.Clear();
            
            _output.Clear();

            _settings.Clear();
        }

        public void Dispose()
        {
            _streamOfNalUnits.Dispose();
            _streamOfNalUnitsFragmented.Dispose();
            _streamOfNalUnitsParams.Dispose();

            _output.Dispose();
        }

        public byte[] ToArray()
        {
            _output.SetLength( 0 );

            _output.WriteAsBinary( _streamOfNalUnitsParams );
            _output.WriteAsBinary( _streamOfNalUnits );

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

            _streamOfNalUnits.WriteAsBinary( _settings.StartCodePrefix );
            _streamOfNalUnits.WriteAsBinary( packet.Payload );
        }

        public void WritePPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H264NalUnit.TryParse( packet.Payload , out var nalUnit ) )
            {
                _streamOfNalUnitsParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnitsParams.WriteAsBinary( packet.Payload );

                _settings.PPS = nalUnit.Payload.ToArray();
            }
        }

        public void WriteSPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H264NalUnit.TryParse( packet.Payload , out var nalUnit ) )
            {
                _streamOfNalUnitsParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnitsParams.WriteAsBinary( packet.Payload );

                _settings.SPS = nalUnit.Payload.ToArray();
            }
        }

        public void WriteStapA( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var aggregate in H264NalUnit.ParseAggregates( packet.Payload ) )
            {
                _streamOfNalUnits.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnits.WriteAsBinary( aggregate );
            }
        }

        public void WriteFuA( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( H264NalUnitFragmentation.TryParse( packet.Payload , out var nalUnit ) )
            {
                if ( H264NalUnitFragmentation.IsStartPacket( ref nalUnit ) )
                {
                    Diagnostics.Debug.EnsureCondition( _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Clear();
                    _streamOfNalUnitsFragmented.WriteAsBinary( _settings.StartCodePrefix );
                    _streamOfNalUnitsFragmented.WriteAsUInt16( H264NalUnitFragmentation.ParseHeader( packet.Payload ) );
                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );
                }
                else if ( H264NalUnitFragmentation.IsDataPacket( ref nalUnit ) )
                {
                    Diagnostics.Debug.EnsureCondition( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );
                }
                else if ( H264NalUnitFragmentation.IsStopPacket( ref nalUnit ) )
                {
                    Diagnostics.Debug.EnsureCondition( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );                    
                    _streamOfNalUnits.WriteAsBinary( _streamOfNalUnitsFragmented );
                    _streamOfNalUnitsFragmented.Clear();
                }
            }
        }
    }
}
