using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    // Introduce an incomplete pipeline interface, it should be review in depth
    // or add a class called RequestManager with the same signature, the same contract
    // and then a the request pipeline interface and inject it into the requestmanager class 
    public interface IRequestPipeLine
    {
        RtspClientResponse Execute( RtspMethod method , string uri , RtspClientRequest request );
    }
}
