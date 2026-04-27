using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public class H264FrameBuilder : RtpFrameBuilder
    {
        private readonly H264FrameFactory _frameFactory = new H264FrameFactory();
        
        public byte[] SPS
        {
            get => _frameFactory.SPS;
            set => _frameFactory.SPS = value;
        }

        public byte[] PPS
        {
            get => _frameFactory.PPS;
            set => _frameFactory.PPS = value;
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

            if ( _frameFactory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnMediaBuilded( new RtpMediaBuildedEventArgs( frame ) );
            }
        }
    }
}
