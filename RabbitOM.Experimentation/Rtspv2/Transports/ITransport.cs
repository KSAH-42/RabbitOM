using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public interface ITransport : IDisposable
    {
        int Receive( byte[] buffer , int offset , int count );

        void Send( byte[] buffer , int offset , int count );

        void Close();
    }
}
