using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegQuantizationTableFactory
    {                                               
        private ArraySegment<byte> _table = default;




        public ArraySegment<byte> Table
        {
            get => _table;
            set => _table = value;
        }




        public int GetNumberOfTables()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _table = default;
        }
    }
}
