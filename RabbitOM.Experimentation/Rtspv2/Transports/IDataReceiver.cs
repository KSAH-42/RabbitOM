using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    // this interface will be used to receive data from UDP or Multicast
    public interface IDataReceiver : IDisposable
    {
        bool IsOpened { get; }


        void Open();

        void Close();

        int Receive( byte[] buffer , int offset , int count );
    }
}
