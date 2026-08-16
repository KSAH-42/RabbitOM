using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal interface IDataReceiver : IDisposable
    {
        bool IsOpened { get; }

        void Open();

        void Close();

        byte[] Receive();
    }
}
