using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments
{
    public abstract class JpegSegment
    {
        public abstract void Serialize( JpegSerializationContext context );
    }
}
