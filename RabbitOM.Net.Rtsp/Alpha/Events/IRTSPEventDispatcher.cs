using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    internal interface IRTSPEventDispatcher
    {
        void Dispatch( EventArgs e );

        void RaiseEvent( EventArgs e );
    }
}
