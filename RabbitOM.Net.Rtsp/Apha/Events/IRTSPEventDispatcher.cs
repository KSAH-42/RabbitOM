using System;

namespace RabbitOM.Net.Rtsp.Apha
{
    public interface IRTSPEventDispatcher : IDisposable
    {
        void Dispatch( EventArgs e );

        void RaiseEvent( EventArgs e );
    }
}
