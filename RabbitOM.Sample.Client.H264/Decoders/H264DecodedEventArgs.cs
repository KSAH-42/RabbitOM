using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public class H264DecodedEventArgs : EventArgs
    {
        public H264Surface Surface { get ; }

        internal H264Context Context { get; }
    }
}