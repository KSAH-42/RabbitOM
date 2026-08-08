using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    // this interface will be used to receive data from UDP or Multicast, etc... but not interleaved data interleaved is attached to a tcp connection or a channel socket connection
    public interface IReceiver : IDisposable
    {
        bool IsOpened { get; }

        void Open();

        void Close();

        int Receive( byte[] buffer , int offset , int count );
    }
}
