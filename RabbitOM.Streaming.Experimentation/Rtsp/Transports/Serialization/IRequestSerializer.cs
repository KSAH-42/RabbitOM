using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Serialization
{
    public interface IRequestSerializer
    {
        byte[] Serialize( RtspRequestMessage message );
    }
}
