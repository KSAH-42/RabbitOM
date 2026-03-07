using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation
{
    public abstract class StringRtspHeaderValidator
    {
        public static StringRtspHeaderValidator TokenValidator { get; } = new TokenRtspHeaderValidator();
        
        public abstract bool TryValidate( string value );
    }
}
