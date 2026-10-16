using System;

namespace RabbitOM.Player.Decoder.Codecs
{
    public class DecodedEventArgs : EventArgs
    {
        public DecodedEventArgs( Surface surface )
        {
            Surface = surface;
        }

        public Surface Surface { get ; }
    }
}