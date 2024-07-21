using System;
using System.IO;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Headers
{
	public sealed class JpegDriSegment : JpegSegment
	{
		public static readonly byte[] Marker = new byte[]
		{
			0xFF ,
			0xDD
		};


		public int Value { get; set; }


		protected override byte[] CreateBuffer()
		{
			using ( var stream = new MemoryStream( 6 ) )
			{
				stream.WriteAsBinary( Marker );
				stream.WriteByte( 0x00 );
				stream.WriteByte( 0x04 );
				stream.WriteByte( (byte) ( ( Value >> 8 ) & 0xFF ) );
				stream.WriteByte( (byte) ( Value & 0xFF ) );

				return stream.ToArray();
			}
		}
	}
}

