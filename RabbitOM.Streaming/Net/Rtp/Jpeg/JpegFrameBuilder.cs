using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    public class JpegFrameBuilder : RtpFrameBuilder , IConfigurer<JpegFrameBuilderConfiguration>
    {
        private readonly JpegFrameFactory _factory = new JpegFrameFactory();

        public virtual void Configure( JpegFrameBuilderConfiguration configuration )
        {
            _factory.Configure( configuration );
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
            base.OnPacketAdding( e );

            e.CanContinue &= e.Packet.Type == RtpPacketType.JPEG;
        }

        protected override void OnSequenceCompleted( RtpSequenceEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _factory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnMediaBuilded( new RtpMediaBuildedEventArgs( frame ) );
            }
        }
    }
}
