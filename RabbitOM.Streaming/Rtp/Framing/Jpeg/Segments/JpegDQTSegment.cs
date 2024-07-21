using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments
{
    public sealed class JpegDQTSegment : JpegSegment
    {
        public static readonly byte[] Marker = new byte[]
        {
            0xFF ,
            0xDB
        };

        




        public byte TableNumber { get; set; }
        public ArraySegment<byte> Data { get; set; }






        protected override byte[] CreateBuffer()
        {
            int length = 3 + Data.Count;

            if ( length > 0xFFFF )
            {
                throw new InvalidOperationException( "the length header field is too big" );
            }

            using ( var stream = new MemoryStream( length + 10 ) )
            {
                stream.WriteAsBinary( Marker );
                stream.WriteAsInt16( (Int16) length );
                stream.WriteByte( TableNumber );
                stream.WriteAsBinary( Data );

                return stream.ToArray();
            }
        }
    }
}

