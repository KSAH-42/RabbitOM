using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    public class JpegFrameBuilder : RtpFrameBuilder , IBuilderConfigurer<JpegFrameBuilderConfiguration>
    {
        private readonly JpegFrameFactory _factory = new JpegFrameFactory();

        public void Configure( JpegFrameBuilderConfiguration settings )
        {
            _factory.Configure( settings );
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _factory.Dispose();
            }

            base.Dispose( disposing );
        }
        
        protected override void OnCleared( EventArgs e )
        {
            _factory.Clear();

            base.OnCleared( e );
        }

        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.JPEG;

            // add code to detect resolution settings when rtp extension is detected
            // save it and update factory settings or decide to inject it on the TryCreateFrame method
            // see code method below

            base.OnPacketAdding( e );
        }

        protected override void OnSequenceCompleted( RtpSequenceCompletedEventArgs e )
        {
            base.OnSequenceCompleted( e );

            // see code method upper
            // add code to detect resolution settings when rtp extension is detected
            // save it and update factory settings or decide to inject it on the TryCreateFrame method

            if ( _factory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnBuilded( new RtpBuildEventArgs( frame ) );
            }
        }
    }
}
