// TODO: add support of DON (decoding order number during parsing)
using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    /// <summary>
    /// Represent a H264 frame builder
    /// </summary>
    public sealed class H264FrameBuilder : RtpFrameBuilder
    {
        private readonly H264FrameFactory _frameFactory = new H264FrameFactory();
        
        public void Configure( byte[] pps , byte[] sps )
        {
            _frameFactory.Configure( pps , sps );
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
            e.CanContinue = e.Packet.Type == RtpPacketType.MPEG4 || e.Packet.Type == RtpPacketType.MPEG4_DYNAMIC_A;

            base.OnPacketAdding( e );
        }

        protected override void OnSequenceCompleted( RtpSequenceCompletedEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _frameFactory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnBuild( new RtpFrameBuildedEventArgs( frame ) );
            }
        }
    }
}
