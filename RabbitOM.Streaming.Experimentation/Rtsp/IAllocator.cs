using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public interface IAllocator : IDisposable
    {
        byte[] Rent( int size );

        void Return( byte[] buffer );
    }
}
