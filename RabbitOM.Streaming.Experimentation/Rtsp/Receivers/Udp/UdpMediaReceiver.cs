using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers.Udp
{
    public class UdpMediaReceiver : RtspMediaReceiver, IMediaReceiverConfigurer<UdpMediaReceiverConfiguration>
    {
        public override bool IsCommunicationStarted
        {
            get => throw new NotImplementedException();
        }

        public override bool IsCommunicationStopping
        {
            get => throw new NotImplementedException();
        }

        public override bool IsConnected
        {
            get => throw new NotImplementedException();
        }

        public override bool IsStreamingStarted
        {
            get => throw new NotImplementedException();
        }

        public override bool IsReceivingData
        {
            get => throw new NotImplementedException();
        }






        public void Configure( UdpMediaReceiverConfiguration configuration )
        {
            throw new NotImplementedException();
        }

        public override bool StartCommunication()
        {
            throw new NotImplementedException();
        }

        public override void StopCommunication()
        {
            throw new NotImplementedException();
        }

        public override void StopCommunication(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public override void BeginStopCommunication()
        {
            throw new NotImplementedException();
        }

        public override bool EndStopCommunication(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose( bool disposing )
        {
            
            base.Dispose( disposing );
        }
    }
}
