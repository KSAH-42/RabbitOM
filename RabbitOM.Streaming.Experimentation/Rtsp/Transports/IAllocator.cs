using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public interface IAllocator : IDisposable
    {
        byte[] AllocateBuffer();

        byte[] AllocateBuffer( int size );
    }
}
