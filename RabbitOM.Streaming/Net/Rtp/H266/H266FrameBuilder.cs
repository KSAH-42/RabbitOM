using System;

namespace RabbitOM.Streaming.Net.Rtp.H266
{
    public class H266FrameBuilder : RtpFrameBuilder , IConfigurer<H266FrameBuilderConfiguration>
    {
        private readonly H266FrameFactory _frameFactory = new H266FrameFactory();

        public H266FrameBuilder()
        {
#if !DEBUG
        throw new NotSupportedException("the implementation is not tested with multiple products and server, and it can not be used until this code will be tested with compliant server")
#endif
        }

        public virtual void Configure( H266FrameBuilderConfiguration configuration )
        {
            _frameFactory.Configure( configuration );
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _frameFactory.Dispose();
            }

            base.Dispose( disposing );
        }

        protected override void OnCleared( RtpClearedEventArgs e )
        {
            _frameFactory.Clear();

            base.OnCleared( e );
        }

        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            base.OnPacketAdding( e );

            e.CanContinue &= RtpPacket.IsDynamicType( e.Packet );
        }

        protected override void OnSequenceCompleted( RtpSequenceEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _frameFactory.TryCreate( e.Packets , out var frame ) )
            {
                OnMediaBuilded( new RtpMediaBuildedEventArgs( frame ) );
            }
        }
    }
}
