using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public interface IRequestHandler
    {
        RtspClientResponse SendRequest( RtspClientRequest request );
    }
}
