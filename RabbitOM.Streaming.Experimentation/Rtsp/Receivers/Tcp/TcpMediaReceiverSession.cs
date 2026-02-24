using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Tcp
{
    public class TcpMediaReceiverSession : IMediaStreamingSession
    {
        private readonly RtspMediaReceiver _receiver;

        public TcpMediaReceiverSession( RtspMediaReceiver receiver )
        {
            _receiver = receiver ?? throw new ArgumentNullException( nameof( receiver ) );
        } 

        public bool IsOpened => throw new NotImplementedException();

        public bool IsStreamingStarted => throw new NotImplementedException();

        public bool IsReceivingData => throw new NotImplementedException();

        public TimeSpan IdleTimeout => throw new NotImplementedException();

        public bool CheckStatus()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Open()
        {
            throw new NotImplementedException();
        }

        public bool StartStreaming()
        {
            throw new NotImplementedException();
        }

        public bool StopStreaming()
        {
            throw new NotImplementedException();
        }
    }
}
