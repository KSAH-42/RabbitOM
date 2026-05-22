using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging
{
    // RTSP stream: [M|M|M|I|M|I|M|I|I|I|I|I|M|M|M]
    // where M : message as request or response
    //   and I : intervealed data
    
    // interface marker has been introduce in case of refactory:
    // moving existing classes that inherit directly this interface to individuals structs,
    // and manipulate them as a common base type

    public interface IStreamElement
    {
    }
}
