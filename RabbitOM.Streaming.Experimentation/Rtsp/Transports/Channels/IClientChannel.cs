using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging;

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

        void Send( RtspInterleaveMessage interleavedData );

        RtspResponseMessage Send( RtspRequestMessage request );
    }
}
