using System;

namespace RabbitOM.Streaming.Rtp.Jpeg.Imaging
{
    public sealed partial class JpegStreamWriter : IDisposable
    {
        private readonly RtpStreamWriter _stream = new RtpStreamWriter();

        private readonly JpegQuantizationTableFactory _quantizationTableFactory = new JpegQuantizationTableFactory();

        private readonly JpegStreamWriterSettings _settings = new JpegStreamWriterSettings();








        public JpegStreamWriterSettings Settings
        {
            get => _settings;
        }

        public long Length
        {
            get => _stream.Length;
        }

        public long Position
        {
            get => _stream.Position;
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

        public void SetLength( long value )
        {
            if ( value < 0 )
            {
                throw new ArgumentException( nameof( value ) ); 
            }

            _stream.SetLength( value );
        }

        public void SetPosition( long value )
        {
            if ( value < 0 )
            {
                throw new ArgumentException( nameof( value ) ); 
            }

            _stream.SetPosition( value );
        }

        public void WriteData( ArraySegment<byte> data )
        {
            if ( data.Count <= 0 )
            {
                throw new ArgumentException( nameof( data ) );
            }

            _stream.Write( data );
        }

        public void WriteStartOfImage()
        {
            _stream.Write( StartOfImageMarker );
        }

        public void WriteEndOfImage()
        {
            _stream.Write( EndOfImageMarker );
        }

        public void WriteApplicationJFIF()
        {
            int length = 2 + IdentifierJFIF.Length + 9;

            if ( length > SegmentMaxLength )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            _stream.Write( ApplicationJFIFMarker );
            _stream.WriteUInt16( length );
            _stream.Write( IdentifierJFIF );
            _stream.WriteByte( _settings.VersionMajor );
            _stream.WriteByte( _settings.VersionMinor );
            _stream.WriteByte( _settings.Unit );
            _stream.WriteUInt16( _settings.XDensity );
            _stream.WriteUInt16( _settings.YDensity );
            _stream.WriteByte( 0 );
            _stream.WriteByte( 0 );
        }

        public void WriteRestartInterval( int value )
        {
            if ( value > 0 )
            {
                _stream.Write( RestartIntervalMarker );
                _stream.WriteByte( 0x00 );
                _stream.WriteByte( 0x04 );
                _stream.WriteUInt16( value );
            }
        }

        public void WriteQuantizationTable( ArraySegment<byte> data , int factor )
        {
            if ( data.Count <= 0 )
            {
                data = _quantizationTableFactory.CreateTable( factor );
            }

            if ( data.Count <= 0 )
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
            if ( data.Count <= 0 )
            {
                throw new ArgumentException( nameof( data ) );
            }

            int length = 3 + data.Count;

            if ( length > SegmentMaxLength )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            _stream.Write( QuantizationTableMarker );
            _stream.WriteUInt16( length );
            _stream.WriteByte( tableNumber );
            _stream.Write( data );
        }

        public void WriteStartOfFrame( int type , int width , int height , ArraySegment<byte> quantizationTable , int quantizationFactor )
        {
            if ( type < 0 )
            {
                throw new ArgumentException( nameof( type ) );
            }

            if ( width <= 0 || height <= 0 || _settings.ResolutionFallback.HasValue ) 
            {
                width  = _settings.ResolutionFallback.Value.Width;
                height = _settings.ResolutionFallback.Value.Height;
            }

            if ( width <= 0 || height <= 0 )
            {
                throw new ArgumentException( "No resolution fallback available" , nameof( width ) );
            }

            if ( quantizationTable.Count <= 0 )
            {
                quantizationTable = _quantizationTableFactory.CreateTable( quantizationFactor );
            }

            if ( quantizationTable.Count <= 0 )
            {
                throw new ArgumentException( nameof( quantizationTable ) );
            }

            byte componentParameterA = ( type & 1 ) != 0 ? (byte) 0x22 : (byte) 0x21;

            byte componentParameterB = quantizationTable.Count <= 64 ? (byte) 0x00 : (byte) 0x01;

            _stream.Write( StartOfFrameMarker );
            _stream.WriteByte( 0x00 );
            _stream.WriteByte( 0x11 );
            _stream.WriteByte( 0x08 );
            _stream.WriteUInt16( height );
            _stream.WriteUInt16( width );
            _stream.WriteByte( 0x03 );
            _stream.WriteByte( 0x01 );
            _stream.WriteByte( componentParameterA );
            _stream.WriteByte( 0x00 );
            _stream.WriteByte( 0x02 );
            _stream.WriteByte( 0x11 );
            _stream.WriteByte( componentParameterB );
            _stream.WriteByte( 0x03 );
            _stream.WriteByte( 0x11 );
            _stream.WriteByte( componentParameterB );
        }

        public void WriteStartOfScan()
        {
            int length = 2 + StartOfScanPayload.Length;

            if ( length > SegmentMaxLength )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            _stream.Write( StartOfScanMarker );
            _stream.WriteUInt16( length );
            _stream.Write( StartOfScanPayload );
        }

        public void WriteHuffmanTable( byte[] codes , byte[] symbols , int tableNo , int tableClass )
        {
            if ( codes == null || codes.Length <= 0 )
            {
                throw new ArgumentException( nameof( codes ) );
            }

            if ( symbols == null || symbols.Length <= 0 )
            {
                throw new ArgumentException( nameof( symbols ) );
            }

            int length = 3 + codes.Length + symbols.Length;

            if ( length > SegmentMaxLength )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            _stream.Write( HuffmanTableMarker );
            _stream.WriteUInt16( length );
            _stream.WriteByte( (byte) ( ( tableClass << 4 ) | tableNo ) );
            _stream.Write( codes );
            _stream.Write( symbols );
        }

        public void WriteHuffmanTables()
        {
            WriteHuffmanTable( LuminanceDirectCodeLens , LuminanceDirectSymbols , 0 , 0 );
            WriteHuffmanTable( LuminanceAlternativeCodeLens , LuminanceAlterntativeSymbols , 0 , 1 );
            WriteHuffmanTable( ChrominanceDirectCodeLens , ChrominanceDirectSymbols , 1 , 0 );
            WriteHuffmanTable( ChrominanceAlternativeCodeLens , ChrominanceAlternativeSymbols , 1 , 1 );
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

            _stream.Write( CommentsMarker );
            _stream.WriteUInt16( length );
            _stream.WriteString( text );
        }
    }
}
