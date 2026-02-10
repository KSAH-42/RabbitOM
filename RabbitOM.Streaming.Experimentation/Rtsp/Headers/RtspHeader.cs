using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public abstract class RtspHeader
    {
        public abstract bool TryValidate();
    }
}
