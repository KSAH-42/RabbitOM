using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core
{
    public abstract class StringRtspHeaderValidator
    {
        public static StringRtspHeaderValidator DefaultValidator = new ProtocolStringRtspHeaderValidator();
        public static StringRtspHeaderValidator TokenValidator = new TokenStringRtspHeaderValidator();

        
        public abstract bool TryValidate( string value );
    }
}
