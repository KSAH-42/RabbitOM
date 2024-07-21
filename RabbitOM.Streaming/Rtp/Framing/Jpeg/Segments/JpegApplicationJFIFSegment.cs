using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments
{
    public sealed class JpegApplicationJFIFSegment : JpegSegment
    {
        private static readonly byte[] Marker = new byte[]
        {
            0xFF , 0xE0
        };

        private static readonly byte[] Identifier = new byte[]
        {
            0x4A , 0x46 , 0x49 , 0x46
        };





        public byte VersionMajor { get; set; } = 1;
        public byte VersionMinor { get; set; } = 1;
        public byte Unit { get; set; }
        public Int16 DensityX { get; set; } = 1;
        public Int16 DensityY { get; set; } = 1;
        public byte ThumbailX { get; set; }
        public byte ThumbailY { get; set; }
        public ArraySegment<byte> Data { get; set; }





        public override void Serialize( JpegSerializationContext context )
        {
            int length = 2 + Identifier.Length + 1 + Data.Count;

            if ( length > 0xFFFF )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            context.WriteAsBinary( Marker );
            context.WriteAsUInt16( length );
            context.WriteAsBinary( Identifier );
            context.WriteAsByte( 0x00 );
            context.WriteAsUInt16( VersionMajor );
            context.WriteAsUInt16( VersionMinor );
            context.WriteAsByte( Unit );
            context.WriteAsUInt16( DensityX );
            context.WriteAsUInt16( DensityY );
            context.WriteAsUInt16( ThumbailX );
            context.WriteAsUInt16( ThumbailY );
            context.WriteAsBinary( Data );
        }
    }
}

