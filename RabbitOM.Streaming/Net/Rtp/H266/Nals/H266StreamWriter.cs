using System;

namespace RabbitOM.Streaming.Net.Rtp.H266.Nals
{
    using RabbitOM.Streaming.IO;

    public sealed class H266StreamWriter : IDisposable
    {
        private readonly H266StreamWriterSettings _settings = new H266StreamWriterSettings();
        
        private readonly MemoryStreamBuffer _streamOfNalUnits = new MemoryStreamBuffer();
        
        private readonly MemoryStreamBuffer _streamOfNalUnitsFragmented = new MemoryStreamBuffer();

        private readonly MemoryStreamBuffer _output = new MemoryStreamBuffer();



        
        public H266StreamWriterSettings Settings
        {
            get => _settings;
        }

        public long Length
        {
            get => _streamOfNalUnits.Length;
        }


        


        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public byte[] ToArray()
        {
            throw new NotImplementedException();
        }
        
        public void SetLength( int value )
        {
            throw new NotImplementedException();
        }

        public void Write( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WritePPS( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteSPS( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteVPS( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteAggregation( RtpPacket packet )
        {
            throw new NotImplementedException();
        }

        public void WriteFragmentation( RtpPacket packet )
        {
            throw new NotImplementedException();
        }
    }
}
