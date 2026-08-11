using System;

namespace RabbitOM.Player.Controls
{
    public interface IDataSource
    {
        string GetCodec();

        string GetTransport();

        bool GetConnectionStatus();
        
        long GetClock();

        long GetBytesReceivedPerSecond();

        long GetPacketReceivedPerSecond();

        long GetFrameCountPerSecond();

        long GetFrameWidth();

        long GetFrameHeight();

        long GetPacketsLostCount();

        void Clear();
    }
}
