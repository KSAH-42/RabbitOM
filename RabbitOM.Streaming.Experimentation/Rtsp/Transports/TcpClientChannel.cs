using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class TcpClientChannel : RtspClientChannel
    {
        public override EndPoint EndPoint { get; }

        public override bool IsOpened
        {
            get => throw new NotImplementedException();
        }






        public override void Open()
        {
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
            throw new NotImplementedException();
        }

        public override void Send( in Packet packet )
        {
            throw new NotImplementedException();
        }
    }
}
