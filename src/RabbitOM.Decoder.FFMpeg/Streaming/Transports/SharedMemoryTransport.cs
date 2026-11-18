using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming.Transports
{
	// for side car pattern
	public sealed class SharedMemoryTransport : ITransport
	{
		public bool IsOpened => throw new NotImplementedException();

		public void Open()
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void Write( byte[] buffer , int offset , int count )
		{
			throw new NotImplementedException();
		}
	}
}
