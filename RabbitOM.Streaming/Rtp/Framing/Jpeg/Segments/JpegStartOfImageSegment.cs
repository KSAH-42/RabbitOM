using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments
{
    public sealed class JpegStartOfImageSegment : JpegSegment
    {
        private static readonly byte[] Marker = new byte[]
        {
            0xFF , 0xD8
        };

        public override void Serialize( JpegSerializationContext context )
        {
            context.WriteAsBinary( Marker );
        }
    }
}

