using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegStreamWriter : IDisposable
    {
        private readonly MemoryStream _stream;

        private readonly JpegQuantizationTableFactory _quantizationTableFactory;

        




        public JpegStreamWriter()
        {
            _stream = new MemoryStream();
            _quantizationTableFactory = new JpegQuantizationTableFactory();
        }





       public long Length
        {
            get => _stream.Length;
        }





        public void WriteStartOfImage()
        {
            _stream.Write( JpegConstants.StartOfImage , 0 , JpegConstants.StartOfImage.Length );
        }

        public void WriteApplicationData()
        {
            // the length of the segment is equal to: 2 bytes of length + the number of remaining bytes

            // so here we have 0x0010 and not 0x000E
            // After the length header, the number of remaining bytes is equal to 14
            // and total is 16 => 0x10
            // to calculate the length, the formula is similar to:
            // length = 2 + payload.size 

            _stream.WriteByte( 0xFF ); // start marker msb
            _stream.WriteByte( 0xE0 ); // start marker lsb
            _stream.WriteByte( 0x00 ); // length msb
            _stream.WriteByte( 0x10 ); // length lsb
            _stream.WriteByte( (byte) 'J' );
            _stream.WriteByte( (byte) 'F' );
            _stream.WriteByte( (byte) 'I' );
            _stream.WriteByte( (byte) 'F' );
            _stream.WriteByte( 0x00 ); // it's the end of string, and as as you know any string must be ended by zero
            _stream.WriteByte( 0x01 ); // version major
            _stream.WriteByte( 0x01 ); // version minor
            _stream.WriteByte( 0x00 ); // units
            _stream.WriteByte( 0x00 ); // x density
            _stream.WriteByte( 0x01 ); // x density
            _stream.WriteByte( 0x00 ); // y density
            _stream.WriteByte( 0x01 ); // y density
            _stream.WriteByte( 0x00 ); // x thumbail
            _stream.WriteByte( 0x00 ); // y thumbail
        }

        public void WriteDri( int value )
        {
            if ( value <= 0 )
            {
                return;
            }

            // the length of the segment is equal to: 2 bytes of length + the number of remaining bytes

            _stream.WriteByte( 0xFF ); // start marker msb
            _stream.WriteByte( 0xDD ); // start marker lsb
            _stream.WriteByte( 0x00 ); // length msb
            _stream.WriteByte( 0x04 ); // length lsb
            _stream.WriteByte( (byte) ( (value >> 8) & 0xFF ) );
            _stream.WriteByte( (byte) (  value       & 0xFF ) );
        }

        public void WriteQuantizationTable( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
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
            _stream.Write( JpegConstants.EndOfImage , 0 , JpegConstants.EndOfImage.Length );
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Clear()
        {
            _stream.SetLength( 0 );
        }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }
    }
}
