using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public interface IClientChannel : IDisposable
    {        
        event EventHandler Opened;

        event EventHandler Closed;

        event EventHandler Aborted;



        bool IsOpened { get; }



        void Open();
        
        void Close();

        void Abort();

        void Send( in Packet packet );

        RtspResponseMessage Send( RtspRequestMessage request );
    }
}
