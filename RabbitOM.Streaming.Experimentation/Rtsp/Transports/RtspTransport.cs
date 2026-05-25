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

        
        

        public bool IsOpened
        {
            get => throw new NotImplementedException();
        }




        public int Send( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException();
        }

        public int Receive( byte[] buffer , int offset , int count )
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
