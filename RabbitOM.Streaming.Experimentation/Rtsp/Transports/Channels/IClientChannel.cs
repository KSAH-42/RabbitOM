using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public interface IClientChannel : IDisposable
    {
        event EventHandler Opened;

        event EventHandler Closed;

        event EventHandler Aborted;

        event EventHandler<RtspMessageEventArgs> MessageReceived;



        bool IsOpened { get; }



        void Open();
        
        void Close();

        void Abort();

        void SendMessage( RtspInterleaveMessage interleavedData );

        RtspResponseMessage SendMessage( RtspRequestMessage request );
    }
}
