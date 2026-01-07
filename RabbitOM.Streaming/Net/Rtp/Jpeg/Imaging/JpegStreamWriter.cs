using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging
{
    using RabbitOM.Streaming.IO;

    /// <summary>
    /// Represent a stream writer class
    /// </summary>
    public sealed partial class JpegStreamWriter : IDisposable
    {
        private readonly MemoryStreamBuffer _stream = new MemoryStreamBuffer();

        private readonly JpegQuantizationTableFactory _quantizationTableFactory = new JpegQuantizationTableFactory();
        
        private readonly JpegStreamWriterSettings _settings = new JpegStreamWriterSettings();








        /// <summary>
        /// Gets the settings
        /// </summary>
        public JpegStreamWriterSettings Settings
        {
            get => _settings;
        }

        /// <summary>
        /// Gets the length
        /// </summary>
        public long Length
        {
            get => _stream.Length;
        }

        /// <summary>
        /// Gets the position
        /// </summary>
        public long Position
        {
            get => _stream.Position;
        }








        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _stream.Dispose();
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _stream.Clear();
        }

        /// <summary>
        /// Create a bytes array from the stream
        /// </summary>
        /// <returns>returns a bytes array</returns>
        public byte[] ToArray()
        {
            return _stream.ToArray();
        }

        /// <summary>
        /// Set the length
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentException"/>
        public void SetLength( long value )
        {
            if ( value < 0 )
            {
                throw new ArgumentException( nameof( value ) ); 
            }

            _stream.SetLength( value );
        }

        /// <summary>
        /// Set the position
        /// </summary>
        /// <param name="value">the value</param>
        /// <exception cref="ArgumentException"/>
        public void SetPosition( long value )
        {
            if ( value < 0 )
            {
                throw new ArgumentException( nameof( value ) ); 
            }

            _stream.SetPosition( value );
        }

        /// <summary>
        /// Write data 
        /// </summary>
        /// <param name="data">the data</param>
        /// <exception cref="ArgumentException"/>
        public void WriteData( in ArraySegment<byte> data )
        {
            if ( data.Count <= 0 )
            {
                throw new ArgumentException( nameof( data ) );
            }

            _stream.Write( data );
        }

        /// <summary>
        /// Write a start of image marker
        /// </summary>
        public void WriteStartOfImage()
        {
            _stream.Write( StartOfImageMarker );
        }

        /// <summary>
        /// Write an end of image marker
        /// </summary>
        public void WriteEndOfImage()
        {
            _stream.Write( EndOfImageMarker );
        }

        /// <summary>
        /// Write the application zero data segment
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
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

        /// <summary>
        /// Write DRI segment
        /// </summary>
        /// <param name="value">the value</param>
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
        
        /// <summary>
        /// Write the quantiztion table segment
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="factor">the factor</param>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        ///     <para>This method split a table in multiple in case if it is too long</para>
        /// </remarks>
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

        /// <summary>
        /// Write the quantization table
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="tableNumber">the table number</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        public void WriteQuantizationTable( in ArraySegment<byte> data , byte tableNumber )
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

        /// <summary>
        /// Write a start of frame segment
        /// </summary>
        /// <param name="type">the type</param>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        /// <param name="quantizationTable">the quantization table</param>
        /// <param name="quantizationFactor">the quantization factor</param>
        /// <exception cref="ArgumentException"/>
        public void WriteStartOfFrame( int type , int width , int height , ArraySegment<byte> quantizationTable , int quantizationFactor )
        {
            if ( type < 0 )
            {
                throw new ArgumentException( nameof( type ) );
            }

            if ( width <= 0 ) 
            {
                width = _settings.ResolutionFallBack?.Width ?? 0;
            }

            if ( height <= 0 )
            {
                height = _settings.ResolutionFallBack?.Height ?? 0;
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

        /// <summary>
        /// Write a start of scan segment
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
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

        /// <summary>
        /// Write a huffman table
        /// </summary>
        /// <param name="codes">the codes</param>
        /// <param name="symbols">the symbols</param>
        /// <param name="tableNo">the table number</param>
        /// <param name="tableClass">the table class type</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
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

        /// <summary>
        /// Write the default huffman tables
        /// </summary>
        public void WriteHuffmanTables()
        {
            WriteHuffmanTable( LuminanceDirectCodeLens , LuminanceDirectSymbols , 0 , 0 );
            WriteHuffmanTable( LuminanceAlternativeCodeLens , LuminanceAlterntativeSymbols , 0 , 1 );
            WriteHuffmanTable( ChrominanceDirectCodeLens , ChrominanceDirectSymbols , 1 , 0 );
            WriteHuffmanTable( ChrominanceAlternativeCodeLens , ChrominanceAlternativeSymbols , 1 , 1 );
        }

        /// <summary>
        /// Write the comments segment
        /// </summary>
        /// <param name="text">the text</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
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
