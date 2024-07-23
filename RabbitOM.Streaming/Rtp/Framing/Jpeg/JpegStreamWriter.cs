using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    // TODO: add quantization factory class and handle changes during writes

    public sealed class JpegStreamWriter : IDisposable
    {
        private static readonly byte[] StartOfImageMarker      = new byte[] { 0xFF , 0xD8 };
        private static readonly byte[] EndOfImageMarker        = new byte[] { 0xFF , 0xD9 };
        private static readonly byte[] ApplicationJFIFMarker   = new byte[] { 0xFF , 0xE0 };
        private static readonly byte[] DriMarker               = new byte[] { 0xFF , 0xDD };
        private static readonly byte[] QuantizationTableMarker = new byte[] { 0xFF , 0xDB };
        private static readonly byte[] StartOfScanMarker       = new byte[] { 0xFF , 0xDA };
        private static readonly byte[] StartOfFrameMarker      = new byte[] { 0xFF , 0xC0 };
        private static readonly byte[] HuffmanMarker           = new byte[] { 0xFF , 0xC4 };
        private static readonly byte[] CommentsMarker          = new byte[] { 0xFF , 0xFE };
        private static readonly byte[] IdenitifierJFIF         = new byte[] { 0x4A , 0x46 , 0x49 , 0x46 , 0x00 };
        
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
                throw new InvalidOperationException( "the length header field is too big" );

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

        public void WriteStartOfFrame( int type , int width , int height )
        {
            if ( type < 0 )
                throw new ArgumentException( nameof( width ) );

            if ( width < 0 )
                throw new ArgumentException( nameof( width ) );

            if ( height < 0 )
                throw new ArgumentException( nameof( height ) );

            _stream.WriteAsBinary( StartOfFrameMarker );
            _stream.WriteAsByte( 0x00 );
            _stream.WriteAsByte( 0x11 );
            _stream.WriteAsByte( 0x08 );
            _stream.WriteAsUInt16( height );
            _stream.WriteAsUInt16( width );
            _stream.WriteAsByte( 0x03 );
            _stream.WriteAsByte( 0x01 );
            _stream.WriteAsByte( ( type & 1 ) != 0 ? (byte) 0x22 : (byte) 0x21 );
            _stream.WriteAsByte( 0x00 );
            _stream.WriteAsByte( 0x02 );
            _stream.WriteAsByte( 0x11 );
            _stream.WriteAsByte( 0x00 ); // TODO: parameterize it like this: qtablesCount == 1 ? (byte)0x00 : (byte)0x01;
            _stream.WriteAsByte( 0x03 );
            _stream.WriteAsByte( 0x11 );
            _stream.WriteAsByte( 0x00 ); // TODO: parameterize it like this: qtablesCount == 1 ? (byte)0x00 : (byte)0x01;
        }

        public void WriteStartHuffmanTables()
        {
            WriteStartHuffmanTable( JpegConstants.LumDcCodelens , JpegConstants.LumDcSymbols , 0 , 0 );
            WriteStartHuffmanTable( JpegConstants.LumAcCodelens , JpegConstants.LumAcSymbols , 0 , 1 );
            WriteStartHuffmanTable( JpegConstants.ChmDcCodelens , JpegConstants.ChmDcSymbols , 0 , 2 );
            WriteStartHuffmanTable( JpegConstants.ChmAcCodelens , JpegConstants.ChmAcSymbols , 0 , 3 );
        }

        public void WriteStartHuffmanTable( byte[] codes , byte[] symbols , int tableNo , int tableClass )
        {
            if ( codes == null || codes.Length == 0 )
                throw new ArgumentException( nameof( codes ) );

            if ( symbols == null || symbols.Length == 0 )
                throw new ArgumentException( nameof( symbols ) );

            _stream.WriteAsBinary( HuffmanMarker );
            _stream.WriteAsByte( 0 );
            _stream.WriteAsByte( (byte) ( 3 + codes.Length + symbols.Length ) );
            _stream.WriteAsByte( (byte) ( ( tableClass << 4 ) | tableNo ) );
            _stream.WriteAsBinary( codes );
            _stream.WriteAsBinary( symbols );
        }

        public void WriteStartOfScan()
        {
            _stream.WriteAsBinary( StartOfScanMarker );
            _stream.WriteAsByte( 0x00 );
            _stream.WriteAsByte( 0x0C );
            _stream.WriteAsByte( 0x03 );
            _stream.WriteAsByte( 0x01 );
            _stream.WriteAsByte( 0x00 );
            _stream.WriteAsByte( 0x02 );
            _stream.WriteAsByte( 0x11 );
            _stream.WriteAsByte( 0x03 );
            _stream.WriteAsByte( 0x11 );
            _stream.WriteAsByte( 0x00 );
            _stream.WriteAsByte( 0x3F );
            _stream.WriteAsByte( 0x00 );
        }

        public void WriteImageData( ArraySegment<byte> data )
        {
            if ( data.Count == 0 )
                throw new ArgumentException( nameof( data ) );

            _stream.WriteAsBinary( data );
        }

        public void WriteEndOfImage()
        {
            _stream.WriteAsBinary( EndOfImageMarker );
        }

        public void WriteComments( string text )
        {
            if ( string.IsNullOrWhiteSpace( text ) )
                throw new ArgumentNullException( nameof( text ) );

            int length = 2 + EndOfImageMarker.Length + text.Length;

            if ( length > MaximumLength )
                throw new InvalidOperationException( "the length header field is too big" );

            _stream.WriteAsBinary( CommentsMarker );
            _stream.WriteAsUInt16( length );
            _stream.WriteAsString( text );
        }
    }
}
