using System;

namespace RabbitOM.Streaming.RtspV2.Headers.DataTypes
{
    public sealed class StringRtspHeaderValueCollection : RtspHeaderValueCollection<string>
    {
        protected override bool OnValidate( string value )
        {
            return RtspHeaderValueValidator.IsWellFormed( value );
        }
    }
}
