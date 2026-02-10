using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public abstract class RtspMediaReceiverConfiguration
    {
        public Uri Uri { get; }
        public TimeSpan ReceiveTimeout { get; }
        public TimeSpan SendTimeout { get; }
        public TimeSpan RetryInterval { get; }
        public TimeSpan HeartBeatTimeout { get; }
        public RtspMethod HeartBeatMethod { get; }
        public MediaContentType ContentType { get; }
    }
}
