using System;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class TcpTransport : ITransport
    {
        private readonly Socket _socket;




        public TcpTransport( Socket socket )
        {
            _socket = socket ?? throw new ArgumentNullException();
        }

        
        
        

        public bool IsOpened
        {
            get => throw new NotImplementedException();
        }

        public bool CanWrite
        {
            get => throw new NotImplementedException();
        }

        public bool CanReceive
        {
            get => throw new NotImplementedException();
        }






        public int WriteBytes( byte[] buffer )
        {
            throw new NotImplementedException();
        }

        public int WriteBytes( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException();
        }

        public int ReceiveBytes( byte[] buffer )
        {
            throw new NotImplementedException();
        }

        public int ReceiveBytes( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
