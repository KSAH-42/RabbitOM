using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Models
{
    // RTSP stream: [M|M|M|I|M|I|M|I|I|I|I|I|M|M|M]
    // where M : message as request or response
    //   and I : intervealed data
    
    // interface marker has been in case of refactory and 
    // move existing class that inherit directly this interface, to struct,
    // moving class to struct and manipulate a common base type

    public interface IStreamElement
    {
    }
}
