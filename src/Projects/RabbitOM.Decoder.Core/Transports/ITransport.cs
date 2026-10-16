using System;

namespace RabbitOM.Decoder.Core.Transports
{
	public interface ITransport : IDisposable
	{
		bool IsOpened { get; }
		void Open();
		void Close();
		void Write( byte[] buffer , int offset , int count );
	}
}
