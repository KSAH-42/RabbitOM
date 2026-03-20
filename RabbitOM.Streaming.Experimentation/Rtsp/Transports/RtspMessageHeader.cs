using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public struct RtspMessageHeader
    {
        public RtspMessageHeader( string name , string[] values )
        {
            Name = name;
            Values = values;
        }

        public string Name { get; }

        public string[] Values { get; }
    }
}
