using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments
{
    public sealed class JpegDriSegment : JpegSegment
    {
        private static readonly byte[] Marker = new byte[]
        {
            0xFF , 0xDD
        };


        public int Value { get; set; }


        public override void Serialize( JpegSerializationContext context )
        {
            context.WriteAsBinary( Marker );
            context.WriteAsByte( 0x00 );
            context.WriteAsByte( 0x04 );
            context.WriteAsByte( (byte) ( ( Value >> 8 ) & 0xFF ) );
            context.WriteAsByte( (byte) ( Value & 0xFF ) );
        }
    }
}

