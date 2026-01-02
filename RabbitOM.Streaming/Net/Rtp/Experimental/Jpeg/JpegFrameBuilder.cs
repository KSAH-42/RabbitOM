using System;

namespace RabbitOM.Streaming.Net.Rtp.Experimental.Jpeg
{
    using RabbitOM.Streaming.Net.Rtp.Jpeg;

    public sealed class JpegFrameBuilder : FrameBuilder
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

        protected override void OnPacketAdding( PacketAddingEventArgs e )
        {
            e.Continue = e.Packet.Type == RtpPacketType.JPEG;

            base.OnPacketAdding( e );
        }

        protected override void OnSequenceCompleted( SequenceCompletedEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _factory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnFrameReceived( new FrameReceivedEventArgs( frame ) );
            }
        }

        protected override void OnCleared( ClearedEventArgs e )
        {
            base.OnCleared( e );

            _factory.Clear();
        }
    }
}
