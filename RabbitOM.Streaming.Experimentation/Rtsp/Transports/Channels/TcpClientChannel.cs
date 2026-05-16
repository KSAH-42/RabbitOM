using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class TcpClientChannel : RtspClientChannel
    {
        public override EndPoint EndPoint 
        { 
            get => throw new NotImplementedException();
        }

        public override bool IsOpened
        {
            get => throw new NotImplementedException();
        }






        public override void Open()
        {
            // _socket = new Socket( ... );
            // _socket.Connect(); :!
            // _requestManager = new ClientRequestManager( this.Endpoint.IsSecured ? new SSLTcpBinaryChannel( _socket ) : new TcpBinaryChannel( _socket ) );
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override RtspResponseMessage Send( RtspRequestMessage request )
        {
            // return _requestManager.SendRequest( request );
            throw new NotImplementedException();
        }

        public override void Send( in Packet packet )
        {
            // _requestManager.SendPacket( packet );
            throw new NotImplementedException();
        }
    }
}
