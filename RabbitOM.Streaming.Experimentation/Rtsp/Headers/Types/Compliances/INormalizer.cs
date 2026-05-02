using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public interface INormalizer<T>
    {
        T Normalize( T value );
    }
}
