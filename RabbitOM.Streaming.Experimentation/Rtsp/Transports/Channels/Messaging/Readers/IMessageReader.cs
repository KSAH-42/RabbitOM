using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    // stream can be: RRRIIIRIQRIIIIIIIIIIIQIIIIRIIIIIIIII
    // where R: response from S
    // where Q: request from S
    // where I: interleaved data from S

    public interface IMessageReader
    {
        RtspMessage ReadMessage();
    }
}
