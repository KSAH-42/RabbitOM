using System;

namespace RabbitOM.Net.RtspV2.Receivers
{
    public class TcpMediaReceiver : RtspMediaReceiver, IMediaReceiverConfigurer<TcpMediaReceiverConfiguration>
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

        public TcpMediaReceiverConfiguration Configuration
        {
            get => throw new NotImplementedException( "To be implemented" );
        }





        public void Configure( TcpMediaReceiverConfiguration configuration )
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
