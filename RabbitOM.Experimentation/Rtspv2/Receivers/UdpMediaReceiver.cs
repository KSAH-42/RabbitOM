using System;

namespace RabbitOM.Streaming.RtspV2.Receivers
{
    public class UdpMediaReceiver : RtspMediaReceiver, IMediaReceiverConfigurer<UdpMediaReceiverConfiguration>
    {
        public override bool IsCommunicationStarted
        {
            get => throw new NotImplementedException( "To be implemented" );
        }

        public override bool IsCommunicationStopping
        {
            get => throw new NotImplementedException( "To be implemented" );
        }

        public override bool IsConnected
        {
            get => throw new NotImplementedException( "To be implemented" );
        }

        public override bool IsStreamingStarted
        {
            get => throw new NotImplementedException( "To be implemented" );
        }

        public override bool IsReceivingData
        {
            get => throw new NotImplementedException( "To be implemented" );
        }

        public UdpMediaReceiverConfiguration Configuration
        {
            get => throw new NotImplementedException( "To be implemented" );
        }





        public void Configure( UdpMediaReceiverConfiguration configuration )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public override bool StartCommunication()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public override void StopCommunication()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public override void Shutdown()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public override bool EndStopCommunication(TimeSpan timeout)
        {
            throw new NotImplementedException( "To be implemented" );
        }

        protected override void Dispose( bool disposing )
        {
            
            base.Dispose( disposing );
        }
    }
}
