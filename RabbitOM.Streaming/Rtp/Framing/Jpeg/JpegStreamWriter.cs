using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed partial class JpegStreamWriter : IDisposable
    {
        private readonly JpegMemoryStream _stream = new JpegMemoryStream();
        private readonly JpegQuantizer _quantizer = new JpegQuantizer();
        private readonly JpegStreamWriterConfiguration _configuration = new JpegStreamWriterConfiguration();




        public JpegStreamWriterConfiguration Configuration
        {
            get => _configuration;
        }

        public long Length
        {
            get => _stream.Length;
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

        public void Write( ArraySegment<byte> data )
        {
            if ( data.Count == 0 )
            {
                throw new ArgumentException( nameof( data ) );
            }

            _stream.WriteAsBinary( data );
        }

        public void WriteStartOfImage()
        {
            _stream.WriteAsBinary( StartOfImageMarker );
        }

        public void WriteEndOfImage()
        {
            _stream.WriteAsBinary( EndOfImageMarker );
        }

        public void WriteApplicationJFIF()
        {
            int length = 2 + IdentifierJFIF.Length + 9;

            if ( length > SegmentMaxLength )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            _stream.WriteAsBinary( ApplicationJFIFMarker );
            _stream.WriteAsUInt16( length );
            _stream.WriteAsBinary( IdentifierJFIF );
            _stream.WriteAsByte( _configuration.VersionMajor );
            _stream.WriteAsByte( _configuration.VersionMinor );
            _stream.WriteAsByte( _configuration.Unit );
            _stream.WriteAsUInt16( _configuration.XDensity );
            _stream.WriteAsUInt16( _configuration.YDensity );
            _stream.WriteAsByte( 0 );
            _stream.WriteAsByte( 0 );
        }

        public void WriteRestartInterval( int value )
        {
            if ( value > 0 )
            {
                _stream.WriteAsBinary( DriMarker );
                _stream.WriteAsByte( 0x00 );
                _stream.WriteAsByte( 0x04 );
                _stream.WriteAsUInt16( value );
            }
        }
        
        public void WriteQuantizationTable( ArraySegment<byte> data , int factor )
        {
            if ( data.Count == 0 )
            {
                data = _quantizer.CreateTable( factor );
            }

            if ( data.Count == 0 )
            {
                throw new InvalidOperationException( "Invalid quantization table" );
            }

            for ( int offset = 0 , tableNumber = 0 ; offset < data.Count && tableNumber <= 0xFF ; offset += 64 , tableNumber ++ )
            {
                int count = 64;

                if ( offset + 64 > data.Count )
                {
                    count = data.Count - offset;
                }

                WriteQuantizationTable( new ArraySegment<byte>( data.Array , data.Offset + offset , count ) , (byte) tableNumber );
            }
        }

        public void WriteQuantizationTable( ArraySegment<byte> data , byte tableNumber )
        {
            if ( data.Count == 0 )
            {
                throw new ArgumentException( nameof( data ) );
            }

            int length = 3 + data.Count;

            if ( length > SegmentMaxLength )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }
            
            _stream.WriteAsBinary( QuantizationTableMarker );
            _stream.WriteAsUInt16( length );
            _stream.WriteAsByte( tableNumber );
            _stream.WriteAsBinary( data );
        }

        public void WriteStartOfFrame( int type , int width , int height , long quantizationTableLength )
        {
            if ( type < 0 )
            {
                throw new ArgumentException( nameof( type ) );
            }

            if ( width < 0 )
            {
                throw new ArgumentException( nameof( width ) );
            }

            if ( height < 0 )
            {
                throw new ArgumentException( nameof( height ) );
            }

            if ( quantizationTableLength < 0 )
            {
                throw new ArgumentException( nameof( quantizationTableLength ) );
            }

            byte componentParameterA = ( type & 1 ) != 0 ? (byte) 0x22 : (byte) 0x21;

            byte componentParameterB = quantizationTableLength <= 64 ? (byte) 0x00 : (byte) 0x01;

            _stream.WriteAsBinary( StartOfFrameMarker );
            _stream.WriteAsByte( 0x00 );
            _stream.WriteAsByte( 0x11 );
            _stream.WriteAsByte( 0x08 );
            _stream.WriteAsUInt16( height );
            _stream.WriteAsUInt16( width );
            _stream.WriteAsByte( 0x03 );
            _stream.WriteAsByte( 0x01 );
            _stream.WriteAsByte( componentParameterA );
            _stream.WriteAsByte( 0x00 );
            _stream.WriteAsByte( 0x02 );
            _stream.WriteAsByte( 0x11 );
            _stream.WriteAsByte( componentParameterB );
            _stream.WriteAsByte( 0x03 );
            _stream.WriteAsByte( 0x11 );
            _stream.WriteAsByte( componentParameterB );
        }

        public void WriteStartOfScan()
        {
            int length = 2 + StartOfScanPayload.Length;

            if ( length > SegmentMaxLength )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            _stream.WriteAsBinary( StartOfScanMarker );
            _stream.WriteAsUInt16( length );
            _stream.WriteAsBinary( StartOfScanPayload );
        }

        public void WriteHuffmanTable( byte[] codes , byte[] symbols , int tableNo , int tableClass )
        {
            if ( codes == null || codes.Length == 0 )
            {
                throw new ArgumentException( nameof( codes ) );
            }

            if ( symbols == null || symbols.Length == 0 )
            {
                throw new ArgumentException( nameof( symbols ) );
            }

            int length = 3 + codes.Length + symbols.Length;

            if ( length > SegmentMaxLength )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            _stream.WriteAsBinary( HuffmanTableMarker );
            _stream.WriteAsUInt16( length );
            _stream.WriteAsByte( (byte) ( ( tableClass << 4 ) | tableNo ) );
            _stream.WriteAsBinary( codes );
            _stream.WriteAsBinary( symbols );
        }

        public void WriteHuffmanTables()
        {
            WriteHuffmanTable( LumDcCodelens , LumDcSymbols , 0 , 0 );
            WriteHuffmanTable( LumAcCodelens , LumAcSymbols , 0 , 1 );
            WriteHuffmanTable( ChmDcCodelens , ChmDcSymbols , 1 , 0 );
            WriteHuffmanTable( ChmAcCodelens , ChmAcSymbols , 1 , 1 );
        }

        public void WriteComments( string text )
        {
            if ( string.IsNullOrWhiteSpace( text ) )
            {
                throw new ArgumentNullException( nameof( text ) );
            }

            int length = 2 + text.Length;

            if ( length > SegmentMaxLength )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            _stream.WriteAsBinary( CommentsMarker );
            _stream.WriteAsUInt16( length );
            _stream.WriteAsString( text );
        }
    }
}
