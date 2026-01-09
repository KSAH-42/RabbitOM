using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    /// <summary>
    /// Represent an MPEG H264 frame builder
    /// </summary>
    public sealed class H264FrameBuilder : RtpFrameBuilder , IBuilderConfigurer<H264FrameBuilderConfiguration>
    {
        private readonly H264FrameFactory _frameFactory = new H264FrameFactory();
        
        public void Configure( H264FrameBuilderConfiguration settings )
        {
            _frameFactory.Configure( settings );
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _frameFactory.Dispose();
            }

            base.Dispose( disposing );
        }

        protected override void OnCleared( EventArgs e )
        {
            _frameFactory.Clear();

            base.OnCleared( e );
        }

        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            e.CanContinue = RtpPacket.IsDynamicType( e.Packet );

            base.OnPacketAdding( e );
        }

        protected override void OnSequenceCompleted( RtpSequenceCompletedEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _frameFactory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnBuilded( new RtpBuildEventArgs( frame ) );
            }
        }
    }
}
