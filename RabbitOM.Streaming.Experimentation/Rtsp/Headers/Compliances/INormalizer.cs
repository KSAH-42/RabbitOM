using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances
{
    public interface INormalizer<T>
    {
        T Normalize( T value );
    }
}
