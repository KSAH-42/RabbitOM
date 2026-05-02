using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public interface IValueChecker<T>
    {
        bool CheckValue( T value );

        T EnsureValue( T value );
    }
}
