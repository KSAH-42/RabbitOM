using System;

namespace RabbitOM.Decoder.FFMpeg.Codecs
{
    public class RenderEventArgs : EventArgs
    {
        public RenderEventArgs( Image image )
        {
            Image = image ?? throw new ArgumentNullException( nameof( image ) );
        }

        public Image Image { get ; }
    }
}