using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers.Events
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
