using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public abstract class RtspStream : Stream
    {
        public abstract int BufferingSize { get; }
        public abstract int PeekByte();
        public abstract string ReadLine();
    }
}
