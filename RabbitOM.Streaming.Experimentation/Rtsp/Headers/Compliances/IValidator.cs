using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances
{
    public interface IValidator<T>
    {
        bool TryValidate( T value );
    }
}
