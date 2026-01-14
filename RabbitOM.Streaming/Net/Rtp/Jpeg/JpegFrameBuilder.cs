using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    public class JpegFrameBuilder : RtpFrameBuilder , IConfigurer<JpegFrameBuilderConfiguration>
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
        
        protected override void OnCleared( RtpClearedEventArgs e )
        {
            _factory.Clear();

            base.OnCleared( e );
        }

        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.JPEG;

            base.OnPacketAdding( e );
        }

        protected override void OnSequenceCompleted( RtpSequenceCompletedEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _factory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnBuilded( new RtpMediaBuildedEventArgs( frame ) );
            }
        }
    }
}
