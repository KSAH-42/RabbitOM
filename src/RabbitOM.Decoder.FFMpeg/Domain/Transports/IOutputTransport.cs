using System;

namespace RabbitOM.Decoder.FFMpeg.Domain.Transports
{
	public interface IOutputTransport : IDisposable
	{
		bool IsOpened { get; }
		void Open();
		void Close();
		void Write( byte[] buffer , int offset , int count );
	}
}
