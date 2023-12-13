using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public class RTSPStreamingStatusChangedEventArgs : EventArgs
    {
        private readonly RTSPStreamingStatus _status;

        public RTSPStreamingStatusChangedEventArgs( RTSPStreamingStatus status )
        {
            _status = status;
        }

        public RTSPStreamingStatus Status
        {
            get => _status;
        }
    }
}
