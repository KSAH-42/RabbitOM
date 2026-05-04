using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Serialization
{
    public interface IRequestSerializer
    {
        ArraySegment<byte> Serialize( RtspRequestMessage message );
    }
}
