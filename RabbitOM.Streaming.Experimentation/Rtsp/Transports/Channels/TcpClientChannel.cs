using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging;

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

        public override void Send( RtspInterleaveMessage interleavedData )
        {
            throw new NotImplementedException();
        }

        public override RtspResponseMessage Send( RtspRequestMessage request )
        {
            throw new NotImplementedException();
        }
    }
}
