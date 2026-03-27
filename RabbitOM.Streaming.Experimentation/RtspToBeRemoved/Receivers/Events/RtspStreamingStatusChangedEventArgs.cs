using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Receivers
{
    public class RtspStreamingStatusChangedEventArgs : EventArgs
    {
        public RtspStreamingStatusChangedEventArgs( StreamingStatus status  )
        {
            Status = status;
        }

        public StreamingStatus Status { get; }
    }
}
