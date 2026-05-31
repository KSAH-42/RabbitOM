using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    // stream could be: RIIRIRIIIRIQIRIIIIIIIIIIIQIIIIRIIIIIIIII
    // where R: response from S
    // where Q: request from S (see rfc the can server request to socket client not listening socket; see OPTIONS: S->C )
    // where I: interleaved data from S
    // the stream buffer window must allow to grab a certains amount of data like this:
    // [0..N/2]
    // [0..N/2]
    // > and iterate until to trigger a socket reading operation after reaching the windows size of the inner buffer
    // normally the grabbing could be something like this
    // [0..A]
    // [A..B]
    // [B..H]
    // [H..N]

    public interface IMessageReader
    {
        RtspMessage ReadMessage();
    }
}
