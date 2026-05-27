using System;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspTransport : ITransport
    {
        private readonly Socket _socket;

        public RtspTransport( Socket socket )
        {
            _socket = socket ?? throw new ArgumentNullException();
        }

        public void Send( byte[] buffer , int offset , int count )
        {
            _socket.Send( buffer , offset , count , SocketFlags.None );
        }

        public int Receive( byte[] buffer , int offset , int count )
        {
            return _socket.Receive( buffer , offset , count , SocketFlags.None );
        }

        public void Close()
        {
            _socket.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
