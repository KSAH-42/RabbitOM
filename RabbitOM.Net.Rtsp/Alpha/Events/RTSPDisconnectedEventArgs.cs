using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    internal enum RTSPDisconnectReason
    {
        CommunicationClosed = 0 ,

        CommunicationLost
    }

    internal class RTSPDisconnectedEventArgs : EventArgs
    {
        private readonly RTSPDisconnectReason _reason;

        public RTSPDisconnectedEventArgs( RTSPDisconnectReason reason )
        {
            _reason = reason;
        }

        public RTSPDisconnectReason Reason
        {
            get => _reason;
        }
    }
}
