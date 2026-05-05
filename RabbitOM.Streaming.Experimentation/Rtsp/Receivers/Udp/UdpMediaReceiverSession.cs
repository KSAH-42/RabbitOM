using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Udp
{
    public sealed class UdpMediaReceiverSession : IMediaSession
    {
        public bool IsOpened => throw new NotImplementedException();

        public bool IsStreamingStarted => throw new NotImplementedException();

        public bool IsReceivingData => throw new NotImplementedException();

        public TimeSpan PingInteral => throw new NotImplementedException();

        public TimeSpan RetryInteral => throw new NotImplementedException();




        public bool Open()
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

        public bool SendHeartBeat()
        {
            throw new NotImplementedException();
        }

        public bool StartStreaming()
        {
            throw new NotImplementedException();
        }

        public void StopStreaming()
        {
            throw new NotImplementedException();
        }
    }
}
