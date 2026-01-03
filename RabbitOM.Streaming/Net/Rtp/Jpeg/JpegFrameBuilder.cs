using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    public class JpegFrameBuilder : RtpFrameBuilder
    {
        private readonly JpegFrameFactory _factory = new JpegFrameFactory();

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

        protected override void OnFilteringPacket( RtpFilteringPacketEventArgs e )
        {
            e.CanContinue = e.Packet.Type == RtpPacketType.JPEG;

            base.OnFilteringPacket( e );
        }

        protected override void OnSequenceCompleted( RtpSequenceCompletedEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _factory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnFrameReceived( new RtpFrameReceivedEventArgs( frame ) );
            }
        }
    }
}
