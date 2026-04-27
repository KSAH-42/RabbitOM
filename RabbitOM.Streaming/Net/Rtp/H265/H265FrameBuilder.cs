using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public class H265FrameBuilder : RtpFrameBuilder
    {
        private readonly H265FrameFactory _frameFactory = new H265FrameFactory();

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

        public byte[] VPS
        {
            get => _frameFactory.VPS;
            set => _frameFactory.VPS = value;
        }

        public bool DONL
        {
            get => _frameFactory.DONL;
            set => _frameFactory.DONL = value;
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
