using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public interface ITransport : IDisposable
    {
        bool IsOpened { get; }

        bool CanWrite { get; }

        bool CanReceive { get; }



        int WriteBytes( byte[] buffer );

        int WriteBytes( byte[] buffer , int offset , int count );

        int ReceiveBytes( byte[] buffer );

        int ReceiveBytes( byte[] buffer , int offset , int count );

        void Close();
    }
}
