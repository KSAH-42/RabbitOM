using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    using RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments;

    public sealed class JpegStreamWriter : IDisposable
    {
        private readonly MemoryStream _stream;

        private readonly JpegQuantizationTableFactory _quantizationTableFactory;

        private readonly JpegApplicationJFIFSegment _applicationJFIFSegment;

        private readonly JpegDriSegment _driSegment;

        private readonly JpegDQTSegment _dqtSegment;






        public JpegStreamWriter()
        {
            _stream = new MemoryStream();
            _quantizationTableFactory = new JpegQuantizationTableFactory();
            _applicationJFIFSegment = new JpegApplicationJFIFSegment();
            _driSegment = new JpegDriSegment();
            _dqtSegment = new JpegDQTSegment();
        }





       public long Length
        {
            get => _stream.Length;
        }





        public void WriteStartOfImage()
        {
            _stream.Write( JpegConstants.StartOfImage , 0 , JpegConstants.StartOfImage.Length );
        }

        public void WriteApplicationJFIF()
        {
            _stream.Write( _applicationJFIFSegment.GetBuffer() , 0 , _applicationJFIFSegment.GetBuffer().Length );
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
            WriteQuantizationTable( data , 0 );
        }

        public void WriteQuantizationTable( ArraySegment<byte> data , byte tableNumber )
        {
            _dqtSegment.TableNumber = 0;
            _dqtSegment.Data = data;
            _dqtSegment.ClearBuffer();

            _stream.Write( _dqtSegment.GetBuffer() , 0 , _dqtSegment.GetBuffer().Length );
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
