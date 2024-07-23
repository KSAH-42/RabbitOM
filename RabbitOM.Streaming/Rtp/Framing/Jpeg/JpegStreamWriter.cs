using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegStreamWriter : IDisposable
    {
        private static readonly byte[] StartOfImageMarker = new byte[] { 0xFF , 0xD8 };
        private static readonly byte[] EndOfImageMarker = new byte[] { 0xFF , 0xD9 };
        private static readonly byte[] ApplicationJFIFMarker = new byte[] { 0xFF , 0xE0 };
        private static readonly byte[] DriMarker = new byte[] { 0xFF , 0xDD };
        private static readonly byte[] QuantizationTableMarker = new byte[] { 0xFF , 0xDB };
        private static readonly byte[] IdenitifierJFIF = new byte[] { 0x4A , 0x46 , 0x49 , 0x46 , 0x00 };
        private const int MaximumLength = 0xFFFF;

        private readonly JpegMemoryStream _stream = new JpegMemoryStream();
        private readonly JpegStreamWriterConfiguration _configuration = new JpegStreamWriterConfiguration();

        public JpegStreamWriterConfiguration Configuration
        {
            get => _configuration;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Clear()
        {
            _stream.Clear();
        }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }

        public void WriteStartOfImage()
        {
            _stream.WriteAsBinary( StartOfImageMarker );
        }

        public void WriteApplicationJFIF()
        {
            int length = 2 + IdenitifierJFIF.Length + 9;

            if ( length > MaximumLength )
                throw new InvalidOperationException();

            _stream.WriteAsBinary( ApplicationJFIFMarker );
            _stream.WriteAsUInt16( length );
            _stream.WriteAsBinary( IdenitifierJFIF );
            _stream.WriteAsByte( _configuration.VersionMajor );
            _stream.WriteAsByte( _configuration.VersionMinor );
            _stream.WriteAsByte( _configuration.Unit );
            _stream.WriteAsUInt16( _configuration.XDensity );
            _stream.WriteAsUInt16( _configuration.YDensity );
            _stream.WriteAsByte( 0 );
            _stream.WriteAsByte( 0 );
        }

        public void WriteDri( int value )
        {
            if ( value > 0 )
            {
                _stream.WriteAsBinary( DriMarker );
                _stream.WriteAsByte( 0x00 );
                _stream.WriteAsByte( 0x04 );
                _stream.WriteAsUInt16( value );
            }
        }

        public void WriteQuantizationTable( ArraySegment<byte> data , byte tableNumber )
        {
            if ( data.Count == 0 )
                throw new ArgumentException( nameof( data ) );

            int length = 3 + data.Count;

            if ( length > MaximumLength )
                throw new InvalidOperationException( "the length header field is too big" );
            
            _stream.WriteAsBinary( QuantizationTableMarker );
            _stream.WriteAsUInt16( length );
            _stream.WriteAsByte( tableNumber );
            _stream.WriteAsBinary( data );
        }

        public void WriteStartOfFrame( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }

        public void WriteStartHuffmanTable( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }

        public void WriteStartOfScan( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }

        public void WriteEntropy( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }

        public void WriteEndOfImage()
        {
            _stream.WriteAsBinary( EndOfImageMarker );
        }
    }
}
