using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // Name this class as RtspHeaderValue and not RtspHeader for the following reasons:
    // the first one is due to the that fact that a header is compose of a name and it's value like:
    // HEADER-NAME: VALUE
    // Content-Type: 1
    // next, the second reason,like the previous implementation the tostring and the tryparse method, should skip the parse of the first fragment of the header, in otherswords, it should escape the hearname with it's ':' characters in order to keep it simple. and to delegate it in different class
    public abstract class RtspHeaderValue
    {
    }
}
