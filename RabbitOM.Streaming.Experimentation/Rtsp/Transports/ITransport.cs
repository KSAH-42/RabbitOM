using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public interface ITransport : IDisposable
    {
        void Send( byte[] buffer , int offset , int count );

        int Receive( byte[] buffer , int offset , int count );

        void Close();
    }
}
