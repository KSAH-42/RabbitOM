using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation
{
    public abstract class StringValueValidator
    {
        public static StringValueValidator TokenValidator { get; } = new TokenValueValidator();
        
        public abstract bool TryValidate( string value );
    }
}
