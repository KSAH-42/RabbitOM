using System;

namespace RabbitOM.Decoder.FFMpeg.Infrastructure.Transports
{
	using RabbitOM.Decoder.FFMpeg.Domain.Transports;

	public sealed class HttpTransport : ITransport
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
