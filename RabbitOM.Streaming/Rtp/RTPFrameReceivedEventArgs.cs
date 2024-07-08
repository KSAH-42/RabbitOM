using System;

namespace RabbitOM.Streaming.Rtp
{
    public class RTPFrameReceivedEventArgs : EventArgs
    {
        private readonly RTPFrame _frame;

        public RTPFrameReceivedEventArgs( RTPFrame frame )
        {
            _frame = frame ?? throw new ArgumentNullException( nameof( frame ) );
        }

        public RTPFrame Frame 
        {
            get => _frame;
        }
    }
}