using System;

namespace RabbitOM.Net.RtspV2.Receivers
{
    public sealed class TcpMediaReceiverStreamingSession : IMediaStreamingSession
    {
        public bool IsOpened => throw new NotImplementedException( "To be implemented" );

        public bool IsStreamingStarted => throw new NotImplementedException( "To be implemented" );

        public bool IsReceivingData => throw new NotImplementedException( "To be implemented" );

        public TimeSpan PingInteral => throw new NotImplementedException( "To be implemented" );

        public TimeSpan RetryInterval => throw new NotImplementedException( "To be implemented" );




        public bool Open()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public void Close()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public void Dispose()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public bool SendHeartBeat()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public bool StartStreaming()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public void StopStreaming()
        {
            throw new NotImplementedException( "To be implemented" );
        }
    }
}
