using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging;

    public interface IClientChannel : IDisposable
    {        
        event EventHandler Opened;

        event EventHandler Closed;

        event EventHandler Aborted;

        event EventHandler<RtspMessageEventArgs> MessageSended;

        event EventHandler<RtspMessageEventArgs> MessageReceived;



        bool IsOpened { get; }



        void Open();
        
        void Close();

        void Abort();

        void SendMessage( RtspInterleaveMessage interleavedData );

        RtspResponseMessage SendMessage( RtspRequestMessage request );
    }
}
