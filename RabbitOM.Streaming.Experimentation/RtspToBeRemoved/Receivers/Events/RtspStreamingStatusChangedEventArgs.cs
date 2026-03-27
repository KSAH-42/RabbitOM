using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Receivers
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
