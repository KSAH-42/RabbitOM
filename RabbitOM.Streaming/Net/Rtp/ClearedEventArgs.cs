using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class ClearedEventArgs : EventArgs
    {
        public static readonly ClearedEventArgs Default = new ClearedEventArgs();
    }
}
