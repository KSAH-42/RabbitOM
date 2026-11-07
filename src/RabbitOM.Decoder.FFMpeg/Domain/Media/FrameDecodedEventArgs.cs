using System;

namespace RabbitOM.Decoder.FFMpeg.Domain.Media
{
    public class FrameDecodedEventArgs : EventArgs
    {
        public FrameDecodedEventArgs( Surface surface )
        {
            Surface = surface ?? throw new ArgumentNullException( nameof( surface ) );
        }

        public Surface Surface { get ; }
    }
}