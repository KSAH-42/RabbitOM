using System;

namespace RabbitOM.Streaming.RtspV2.Receivers
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
