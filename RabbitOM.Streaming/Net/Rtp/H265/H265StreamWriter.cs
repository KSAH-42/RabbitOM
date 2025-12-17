using RabbitOM.Streaming.Net.Rtp.H265.Headers;
using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public sealed class H265StreamWriter : IDisposable
    {
        private readonly H265StreamWriterSettings _settings = new H265StreamWriterSettings();
        
        private readonly RtpMemoryStream _streamOfNalUnits = new RtpMemoryStream();
        
        private readonly RtpMemoryStream _streamOfNalUnitsFragmented = new RtpMemoryStream();

        private readonly RtpMemoryStream _streamOfNalUnitsParams = new RtpMemoryStream();

        private readonly RtpMemoryStream _output = new RtpMemoryStream();








        
        
        public H265StreamWriterSettings Settings
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

            if ( NalUnit.TryParse( packet.Payload , out var nalUnit ) )
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

            if ( NalUnit.TryParse( packet.Payload , out var nalUnit ) )
            {
                _streamOfNalUnitsParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnitsParams.WriteAsBinary( packet.Payload );

                _settings.SPS = nalUnit.Payload.ToArray();
            }
        }

        public void WriteVPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnit.TryParse( packet.Payload , out var nalUnit ) )
            {
                _streamOfNalUnitsParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnitsParams.WriteAsBinary( packet.Payload );

                _settings.VPS = nalUnit.Payload.ToArray();
            }
        }

        public void WriteAggregation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var aggregate in NalUnit.ParseAggregates( packet.Payload ) )
            {
                _streamOfNalUnits.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfNalUnits.WriteAsBinary( aggregate );
            }
        }

        public void WriteFragmentation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnitFragmentation.TryParse( packet.Payload , out var nalUnit ) )
            {
                if ( NalUnitFragmentation.IsStartPacket( ref nalUnit ) )
                {
                    Diagnostics.Debug.EnsureCondition( _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.Clear();
                    _streamOfNalUnitsFragmented.WriteAsBinary( _settings.StartCodePrefix );
                    _streamOfNalUnitsFragmented.WriteAsUInt16( NalUnitFragmentation.ParseHeader( packet.Payload ) );
                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );
                }
                else if ( NalUnitFragmentation.IsDataPacket( ref nalUnit ) )
                {
                    Diagnostics.Debug.EnsureCondition( ! _streamOfNalUnitsFragmented.IsEmpty );

                    _streamOfNalUnitsFragmented.WriteAsBinary( nalUnit.Payload );
                }
                else if ( NalUnitFragmentation.IsStopPacket( ref nalUnit ) )
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
