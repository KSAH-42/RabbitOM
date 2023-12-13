using System;

namespace RabbitOM.Net.Rtsp.Apha
{
	public interface IRTSPClientDispatcher : IDisposable
	{
		void Dispatch( EventArgs e );

		void RaiseEvent( EventArgs e );
	}
}
