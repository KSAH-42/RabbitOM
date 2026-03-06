using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core
{
    public abstract class StringRtspHeaderComparer 
    {
        public static StringRtspHeaderComparer  IgnoreCaseComparer { get; } = new DefaultStringRtspHeaderComparer ();

        public abstract bool Equals( string x , string y );
    }
}
