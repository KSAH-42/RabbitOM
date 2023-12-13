using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPProtocolErrorEventArgs : RTSPErrorEventArgs
    {
        private readonly RTSPMethod _method;

        private readonly string _message;





        public RTSPProtocolErrorEventArgs( RTSPMethod method )
            : this ( method , string.Empty )
        {
        }

        public RTSPProtocolErrorEventArgs( RTSPMethod method , string message )
        {
            _method  = method;
            _message = message ?? string.Empty;
        }





        public RTSPMethod Method
        {
            get => _method;
        }

        public string Message
        {
            get => _message;
        }
    }
}
