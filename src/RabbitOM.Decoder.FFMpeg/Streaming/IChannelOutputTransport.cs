using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming
{
	public interface IOutputTransport : IDisposable
	{
		bool IsOpened { get; }
		void Open();
		void Close();
		void Write( byte[] buffer , int offset , int count );
	}
}
