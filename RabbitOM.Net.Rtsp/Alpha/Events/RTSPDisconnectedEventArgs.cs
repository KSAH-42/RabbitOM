using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public enum RTSPDisconnectReason
    {
        CommunicationClosed = 0 ,

        CommunicationLost
    }

    public class RTSPDisconnectedEventArgs : EventArgs
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
