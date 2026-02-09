using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public abstract class RtspHeader
    {
        public abstract bool TryValidate();
    }
}
