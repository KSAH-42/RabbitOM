using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public interface IValidator<T>
    {
        bool TryValidate( T value );
    }
}
