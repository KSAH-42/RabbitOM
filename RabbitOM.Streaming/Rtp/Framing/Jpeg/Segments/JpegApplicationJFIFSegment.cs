using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments
{
    public sealed class JpegApplicationJFIFSegment : JpegSegment
    {
        public static readonly byte[] Marker = new byte[]
        {
            0xFF ,
            0xE0
        };

        public static readonly string IdentifierJFIF = "JFIF";






        public string Identifier { get; private set; } = IdentifierJFIF;
        public byte VersionMajor { get; set; } = 1;
        public byte VersionMinor { get; set; } = 1;
        public byte Unit { get; set; }
        public Int16 DensityX { get; set; } = 1;
        public Int16 DensityY { get; set; } = 1;
        public byte ThumbailX { get; set; }
        public byte ThumbailY { get; set; }
        public ArraySegment<byte> ThumbailData { get; set; }






        protected override byte[] CreateBuffer()
        {
            int length = 2 + Identifier.Length + 1 + ThumbailData.Count;

            if ( length > 0xFFFF )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            using ( var stream = new MemoryStream( 100 ) )
            {
                stream.WriteAsBinary( Marker );
                stream.WriteAsInt16( (Int16) length );
                stream.WriteAsString( Identifier );
                stream.WriteByte( 0x00 );
                stream.WriteAsInt16( VersionMajor );
                stream.WriteAsInt16( VersionMinor );
                stream.WriteByte( Unit );
                stream.WriteAsInt16( DensityX );
                stream.WriteAsInt16( DensityY );
                stream.WriteAsInt16( ThumbailX );
                stream.WriteAsInt16( ThumbailY );
                stream.WriteAsBinary( ThumbailData );

                return stream.ToArray();
            }
        }
    }
}

