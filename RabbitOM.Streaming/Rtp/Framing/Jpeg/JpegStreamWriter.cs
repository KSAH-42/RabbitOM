using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    using RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments;

    public sealed class JpegStreamWriter : IDisposable
    {
        private readonly MemoryStream _stream;

        private readonly JpegQuantizationTableFactory _quantizationTableFactory;

        private readonly JpegStartOfImageSegment _startOfImageSegment;

        private readonly JpegApplicationJFIFSegment _applicationJFIFSegment;

        private readonly JpegDriSegment _driSegment;

        private readonly JpegDQTSegment _dqtSegment;

        private readonly JpegEndOfImageSegment _endOfImageSegment;

        private readonly JpegSerializationContext _context;





        public JpegStreamWriter()
        {
            _stream = new MemoryStream();
            _quantizationTableFactory = new JpegQuantizationTableFactory();
            _startOfImageSegment = new JpegStartOfImageSegment();
            _applicationJFIFSegment = new JpegApplicationJFIFSegment();
            _driSegment = new JpegDriSegment();
            _dqtSegment = new JpegDQTSegment();
            _endOfImageSegment = new JpegEndOfImageSegment();
            _context = new JpegSerializationContext( _stream );
        }





       public long Length
        {
            get => _stream.Length;
        }





        public void WriteStartOfImage()
        {
            _startOfImageSegment.Serialize( _context );
        }

        public void WriteApplicationJFIF()
        {
            _applicationJFIFSegment.Serialize( _context );
        }

        public void WriteDri( int value )
        {
            if ( value > 0 )
            {
                _driSegment.Value = value;

                _driSegment.Serialize( _context );
            }
        }

        public void WriteQuantizationTable( ArraySegment<byte> data , byte tableNumber )
        {
            _dqtSegment.TableNumber = tableNumber;
            _dqtSegment.Data = data;

            _dqtSegment.Serialize( _context );
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
            _endOfImageSegment.Serialize( _context );
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
