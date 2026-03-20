using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public class TcpClientChannel : RtspClientChannel
    {
        public override string Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override TimeSpan ReceiveTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override TimeSpan SendTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override bool IsOpened => throw new NotImplementedException();



        public override void Open()
        {
            throw new NotImplementedException();
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override RtspResponseMessage SendMessage( RtspRequestMessage request )
        {
            throw new NotImplementedException();
        }
    }
}
