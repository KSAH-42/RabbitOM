using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public interface IClientChannel : IDisposable
    {        
        event EventHandler Opened;

        event EventHandler Closed;

        event EventHandler Aborted;

        event EventHandler<RtspRequestMessageEventArgs> RequestSended;

        event EventHandler<RtspResponseMessageEventArgs> ResponseReceived;



        bool IsOpened { get; }



        void Open();
        
        void Close();

        void Abort();

        void Send( in Packet packet );

        RtspResponseMessage Send( RtspRequestMessage request );
    }
}
