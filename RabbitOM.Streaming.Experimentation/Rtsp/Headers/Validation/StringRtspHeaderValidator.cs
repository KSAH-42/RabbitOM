using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation
{
    public abstract class StringRtspHeaderValidator
    {
        public static StringRtspHeaderValidator DefaultValidator { get; } = new ProtocolStringRtspHeaderValidator();
        public static StringRtspHeaderValidator TokenValidator { get; } = new TokenStringRtspHeaderValidator();

        
        public abstract bool TryValidate( string value );
    }
}
