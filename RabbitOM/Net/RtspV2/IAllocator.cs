using System;

namespace RabbitOM.Net.RtspV2
{
    public interface IAllocator : IDisposable
    {
        byte[] Rent( int size );

        void Return( byte[] buffer );
    }
}