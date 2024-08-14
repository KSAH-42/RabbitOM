using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    // The following implementation is subject to change or to be removed entirely

    public sealed class H265StreamWriter : IDisposable
    {
        private static readonly byte[] StartPrefix = { 0x00 , 0x00 , 0x00 , 0x01 };



        
        private readonly RtpMemoryStream _stream = new RtpMemoryStream();

     



        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Clear()
        {
            _stream.Clear();
        }

        public void Write( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }

        public void WritePPS( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }

        public void WriteSPS( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }

        public void WriteVPS( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }
        
        public void WriteFragmentation( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }
        
        public void WriteAggregation( ArraySegment<byte> data )
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }
    }
}