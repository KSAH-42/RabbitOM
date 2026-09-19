using System;

namespace RabbitOM.Net.Rtp.Jpeg
{
	public struct JpegRectangle
	{
		public UInt16 Color { get; set; }

		public UInt16 X { get; set; }

		public UInt16 Y { get; set; }

		public UInt16 Width { get; set; }

		public UInt16 Height { get; set; }

		public byte[] ToArray()
		{
			return new byte[]
				{
					(byte)((Color >> 8) & 0xFF), (byte)(Color & 0xFF),
					(byte)((X >> 8) & 0xFF), (byte)(X & 0xFF),
					(byte)((Y >> 8) & 0xFF), (byte)(Y & 0xFF),
					(byte)((Width >> 8) & 0xFF), (byte)(Width & 0xFF),
					(byte)((Height >> 8) & 0xFF), (byte)(Height & 0xFF),
				};
		}
	}
}
