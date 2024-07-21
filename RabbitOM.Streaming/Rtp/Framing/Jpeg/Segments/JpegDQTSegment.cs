using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments
{
    public sealed class JpegDQTSegment : JpegSegment
    {
        private static readonly byte[] Marker = new byte[]
        {
            0xFF , 0xDB
        };


        public byte TableNumber { get; set; }
        public ArraySegment<byte> Data { get; set; }


        public override void Serialize( JpegSerializationContext context )
        {
            int length = 3 + Data.Count;

            if ( length > 0xFFFF )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            context.WriteAsBinary( Marker );
            context.WriteAsUInt16( length );
            context.WriteAsByte( TableNumber );
            context.WriteAsBinary( Data );
        }
    }
}

