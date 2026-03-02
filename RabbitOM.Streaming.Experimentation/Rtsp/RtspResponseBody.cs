using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public abstract class RtspResponseBody
    {
        public abstract int Length { get; }

        public abstract byte[] ReadBytes();

        public abstract byte[] ReadBytes( int count );
    }
}
