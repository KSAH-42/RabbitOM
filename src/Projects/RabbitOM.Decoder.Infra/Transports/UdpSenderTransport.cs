using System;

namespace RabbitOM.Decoder.Infra.Transports
{
	using RabbitOM.Decoder.Core.Transports;

	public sealed class UdpSenderTransport : ITransport
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
