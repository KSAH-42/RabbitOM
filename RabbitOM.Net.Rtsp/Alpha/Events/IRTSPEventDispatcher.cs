using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public interface IRTSPEventDispatcher : IDisposable
    {
        void Dispatch( EventArgs e );

        void RaiseEvent( EventArgs e );
    }
}
