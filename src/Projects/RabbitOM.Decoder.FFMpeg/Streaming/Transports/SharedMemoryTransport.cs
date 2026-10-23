using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming
{
	// TODO: Do we need to move to parent namespace (Streaming) actually there is 2 classes, for this class we can introduce memory namespace and the transport will used theses classes (Decoder.FFMpeg.Streaming.Memory ?)
	public sealed class SharedMemoryTransport : IOutputTransport
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
