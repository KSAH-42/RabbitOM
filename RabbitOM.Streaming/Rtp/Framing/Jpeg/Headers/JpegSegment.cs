using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Headers
{
	public abstract class JpegSegment
	{
		protected byte[] _buffer = null;


		public byte[] GetBuffer()
		{
			if ( _buffer == null )
			{
				_buffer = CreateBuffer();
			}

			return _buffer;
		}


		public void ClearBuffer()
		{
			_buffer = null;
		}

		protected abstract byte[] CreateBuffer();
	}
}
