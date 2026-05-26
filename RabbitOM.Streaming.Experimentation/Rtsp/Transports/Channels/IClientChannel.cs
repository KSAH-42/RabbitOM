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

        RtspResponseMessage SendMessage( RtspRequestMessage request );

        void SendMessage( RtspInterleaveMessage interleavedData );
    }
}
