using RabbitOM.Decoder.FFMpeg.Streaming.Media;
using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming.Media.Decoders
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