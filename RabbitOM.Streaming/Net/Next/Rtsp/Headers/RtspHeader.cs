using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Headers
{
    public abstract class RtspHeader
    {
        public abstract bool TryValidate();
    }
}
