using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation
{
    public abstract class StringValueValidator
    {
        public static StringValueValidator TokenValidator { get; } = new TokenValueValidator();
        public static StringValueValidator UriValidator { get; } = new UriValueValidator();

        public abstract bool TryValidate( string value );
    }
}
