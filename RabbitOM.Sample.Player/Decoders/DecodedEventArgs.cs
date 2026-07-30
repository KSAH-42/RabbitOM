using System;

namespace RabbitOM.Sample.Client.Player.Codecs
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