using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    public class RtspConnectionErrorEventArgs : EventArgs
    {
        private readonly Exception _exception = null;

        public RtspConnectionErrorEventArgs( Exception exception )
        {
            _exception = exception;
        }

        public Exception Exception
        {
            get => _exception;
        }
    }
}
