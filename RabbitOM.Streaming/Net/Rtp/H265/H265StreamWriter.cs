using RabbitOM.Streaming.Net.Rtp.H265.Headers;
using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public sealed class H265StreamWriter : IDisposable
    {
        private readonly H265StreamWriterSettings _settings = new H265StreamWriterSettings();
        
        private readonly RtpMemoryStream _streamOfPackets = new RtpMemoryStream();
        
        private readonly RtpMemoryStream _streamOfFragmentedPackets = new RtpMemoryStream();

        private readonly RtpMemoryStream _streamOfParams = new RtpMemoryStream();








        
        
        public H265StreamWriterSettings Settings
        {
            get => _settings;
        }

        public long Length
        {
            get => _streamOfPackets.Length;
        }

        




        



        public void Clear()
        {
            _streamOfPackets.Clear();
            _streamOfFragmentedPackets.Clear();
            _streamOfParams.Clear();
            _settings.Clear();
        }

        public void Dispose()
        {
            _streamOfPackets.Dispose();
            _streamOfFragmentedPackets.Dispose();
            _streamOfParams.Dispose();
        }

        public byte[] ToArray()
        {
            var output = new RtpMemoryStream();

            output.WriteAsBinary( _streamOfParams );
            output.WriteAsBinary( _streamOfPackets );

            return output.ToArray();
        }
        
        public void SetLength( int value )
        {
            _streamOfPackets.SetLength( value );
        }

        public void Write( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            _streamOfPackets.WriteAsBinary( _settings.StartCodePrefix );
            _streamOfPackets.WriteAsBinary( packet.Payload );
        }

        public void WritePPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnitHeader.TryParse( packet.Payload , out var header ) )
            {
                _streamOfParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfParams.WriteAsBinary( packet.Payload );

                _settings.PPS = header.Payload.ToArray();
            }
        }

        public void WriteSPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnitHeader.TryParse( packet.Payload , out var header ) )
            {
                _streamOfParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfParams.WriteAsBinary( packet.Payload );

                _settings.SPS = header.Payload.ToArray();
            }
        }

        public void WriteVPS( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnitHeader.TryParse( packet.Payload , out var header ) )
            {
                _streamOfParams.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfParams.WriteAsBinary( packet.Payload );

                _settings.VPS = header.Payload.ToArray();
            }
        }

        public void WriteAggregation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            foreach ( var aggregate in NalUnitHeader.ParseAggregates( packet.Payload ) )
            {
                _streamOfPackets.WriteAsBinary( _settings.StartCodePrefix );
                _streamOfPackets.WriteAsBinary( aggregate );
            }
        }

        public void WriteFragmentation( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( NalUnitFragmentationHeader.TryParse( packet.Payload , out var header ) )
            {
                if ( NalUnitFragmentationHeader.IsStartPacket( ref header ) )
                {
                    Diagnostics.Debug.EnsureCondition( _streamOfFragmentedPackets.IsEmpty );

                    _streamOfFragmentedPackets.Clear();
                    _streamOfFragmentedPackets.WriteAsBinary( _settings.StartCodePrefix );
                    _streamOfFragmentedPackets.WriteAsUInt16( NalUnitFragmentationHeader.ParseHeader( packet.Payload ) );
                    _streamOfFragmentedPackets.WriteAsBinary( header.Payload );
                }
                else if ( NalUnitFragmentationHeader.IsDataPacket( ref header ) )
                {
                    Diagnostics.Debug.EnsureCondition( ! _streamOfFragmentedPackets.IsEmpty );

                    _streamOfFragmentedPackets.WriteAsBinary( header.Payload );
                }
                else if ( NalUnitFragmentationHeader.IsStopPacket( ref header ) )
                {
                    Diagnostics.Debug.EnsureCondition( ! _streamOfFragmentedPackets.IsEmpty );

                    _streamOfFragmentedPackets.WriteAsBinary( header.Payload );                    
                    _streamOfPackets.WriteAsBinary( _streamOfFragmentedPackets );
                    _streamOfFragmentedPackets.Clear();
                }
            }
        }
    }
}
