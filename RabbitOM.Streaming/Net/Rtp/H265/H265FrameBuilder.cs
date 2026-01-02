using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent a H265 frame builder
    /// </summary>
    public sealed class H265FrameBuilder : RtpFrameBuilder
    {
        private readonly H265FrameFactory _frameFactory = new H265FrameFactory();

        public void Configure( byte[] pps , byte[] sps , byte[] vps )
        {
            _frameFactory.Configure( pps , sps , vps );
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
            base.OnCleared( e );

            _frameFactory.Clear();
        }

        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            base.OnPacketAdding( e );

            e.Continue = e.Packet.Type == RtpPacketType.MPEG4
                      || e.Packet.Type == RtpPacketType.MPEG4_DYNAMIC_A
                      || e.Packet.Type == RtpPacketType.MPEG4_DYNAMIC_B
                      || e.Packet.Type == RtpPacketType.MPEG4_DYNAMIC_C
                      || e.Packet.Type == RtpPacketType.MPEG4_DYNAMIC_D
                      ;
        }

        protected override void OnSequenceCompleted( RtpSequenceCompletedEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _frameFactory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnFrameReceived( new RtpFrameReceivedEventArgs( frame ) );
            }
        }
    }
}
