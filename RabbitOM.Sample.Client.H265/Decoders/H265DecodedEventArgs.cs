using System;

namespace RabbitOM.Sample.Client.H265.Codecs
{
    public class H265DecodedEventArgs : EventArgs
    {
        public H265DecodedEventArgs( H265Surface surface )
        {
            Surface = surface;
        }

        public H265Surface Surface { get ; }
    }
}