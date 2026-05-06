using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    /// TODO: should handle ssl stream when uri scheme is equals to rtsps don't use a bool something this
    public sealed class TcpClientChannel : RtspClientChannel
    {
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

        public override void Send( in InterleavedPacket packet )
        {
            throw new NotImplementedException();
        }
    }
}
