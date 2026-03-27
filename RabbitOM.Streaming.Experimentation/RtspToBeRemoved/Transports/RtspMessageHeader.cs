using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Transports
{
    public struct RtspMessageHeader
    {
        public RtspMessageHeader( string name , string value )
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }
    }
}
