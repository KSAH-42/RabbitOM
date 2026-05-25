using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    // stream could be: RRRIIIRIQRIIIIIIIIIIIQIIIIRIIIIIIIII
    // where R: response from S
    // where Q: request from S
    // where I: interleaved data to S
    // where S: the magic or the buggy server or maybe the magic buggy server !
    // hey do we need to adapt the reading from unknow rtsp source ?

    public interface IMessageReader
    {
        RtspMessage ReadMessage();
    }
}
