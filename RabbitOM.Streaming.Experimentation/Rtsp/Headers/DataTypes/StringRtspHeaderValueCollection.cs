using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class StringRtspHeaderValueCollection : RtspHeaderValueCollection<string>
    {
        protected override bool OnValidate( string value )
        {
            return RtspHeaderValueValidator.TryEnsureWellFormedToken( value );
        }
    }
}
