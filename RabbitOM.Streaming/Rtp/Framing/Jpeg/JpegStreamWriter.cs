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

        public void WriteApplicationSegments( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
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
