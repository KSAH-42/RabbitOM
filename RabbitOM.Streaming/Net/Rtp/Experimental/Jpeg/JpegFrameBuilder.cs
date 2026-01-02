using System;

namespace RabbitOM.Streaming.Net.Rtp.Experimental.Jpeg
{
    using RabbitOM.Streaming.Net.Rtp.Jpeg;

    public class JpegFrameBuilder : FrameBuilder
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
        
        protected override void OnCleared( ClearedEventArgs e )
        {
            _factory.Clear();

            base.OnCleared( e );
        }

        protected override void OnPacketAdding( PacketAddingEventArgs e )
        {
            base.OnPacketAdding( e );

            e.Continue = e.Packet.Type == RtpPacketType.JPEG;
        }

        protected override void OnSequenceCompleted( SequenceCompletedEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _factory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnFrameReceived( new FrameReceivedEventArgs( frame ) );
            }
        }
    }
}
