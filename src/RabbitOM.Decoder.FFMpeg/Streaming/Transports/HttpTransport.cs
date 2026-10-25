using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming
{
	// TODO: Do we need to move to parent namespace (Streaming) actually there is 2 classes (transport is related to streaming...)
	public sealed class HttpTransport : IOutputTransport
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
