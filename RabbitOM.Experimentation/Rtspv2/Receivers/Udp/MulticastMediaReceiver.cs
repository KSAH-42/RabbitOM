using System;

namespace RabbitOM.Streaming.RtspV2.Receivers.Udp
{
    public class MulticastMediaReceiver : RtspMediaReceiver, IMediaReceiverConfigurer<MulticastMediaReceiverConfiguration>
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

        public MulticastMediaReceiverConfiguration Configuration
        {
            get => throw new NotImplementedException();
        }





        public void Configure( MulticastMediaReceiverConfiguration configuration )
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

        public override void Shutdown()
        {
            throw new NotImplementedException();
        }

        public override bool WaitForShutdown(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose( bool disposing )
        {
            
            base.Dispose( disposing );
        }
    }
}
