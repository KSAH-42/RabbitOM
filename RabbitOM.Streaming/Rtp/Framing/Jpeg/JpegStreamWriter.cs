using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    using RabbitOM.Streaming.Rtp.Framing.Jpeg.Data;

    public sealed class JpegStreamWriter : IDisposable
    {
        private readonly MemoryStream _stream;

        private readonly JpegQuantizationTableFactory _quantizationTableFactory;

        private readonly ApplicationDataJpegSegment _appDataSegment;

        private readonly DriJpegSegment _driSegment;






        public JpegStreamWriter()
        {
            _stream = new MemoryStream();
            _quantizationTableFactory = new JpegQuantizationTableFactory();
            _appDataSegment = new ApplicationDataJpegSegment();
            _driSegment = new DriJpegSegment();
        }





       public long Length
        {
            get => _stream.Length;
        }





        public void WriteStartOfImage()
        {
            _stream.Write( JpegConstants.StartOfImage , 0 , JpegConstants.StartOfImage.Length );
        }

        public void WriteApplicationInfo()
        {
            _stream.Write( _appDataSegment.GetBuffer() , 0 , _appDataSegment.GetBuffer().Length );
        }

        public void WriteDri( int value )
        {
            if ( value <= 0 )
            {
                return;
            }

            if ( _driSegment.Value != value )
            {
                _driSegment.Value = value;
                _driSegment.ClearBuffer();
            }

            _stream.Write( _driSegment.GetBuffer() , 0 , _driSegment.GetBuffer().Length );
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
