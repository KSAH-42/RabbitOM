using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    // Introduce an incomplete pipeline interface, it should be review in depth
    public interface IRequestPipeLine
    {
        RtspClientResponse Execute( RtspMethod method , string uri , RtspClientRequest request );
    }
}
