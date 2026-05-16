using System;
using System.Net.Sockets;
using System.Net.Security;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class SecureTcpTransport : ITransport
    {
        private readonly Socket _socket;
        private readonly SslStream _stream;




        public SecureTcpTransport( Socket socket )
        {
            _socket = socket ?? throw new ArgumentNullException();
            _stream = new SslStream( new NetworkStream( socket ) );
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
