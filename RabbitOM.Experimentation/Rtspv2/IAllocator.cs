using System;

namespace RabbitOM.Streaming.RtspV2
{
    public interface IAllocator : IDisposable
    {
        byte[] Rent( int size );

        void Return( byte[] buffer );
    }
}
