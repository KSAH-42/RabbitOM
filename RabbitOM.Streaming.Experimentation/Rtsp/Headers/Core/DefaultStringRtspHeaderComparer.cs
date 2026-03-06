using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core
{
    public sealed class DefaultStringRtspHeaderComparer : StringRtspHeaderComparer 
    {
        public override bool Equals( string x , string y )
        {
            return string.Equals( x , y , StringComparison.OrdinalIgnoreCase );
        }
    }
}
