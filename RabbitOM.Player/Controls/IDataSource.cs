using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public interface IDataSource
    {
        string GetCodec();

        bool GetConnectionStatus();

        long GetBytesReceivedPerSecond();

        long GetPacketReceivedPerSecond();

        long GetFrameCountPerSecond();

        long GetFrameWidth();

        long GetFrameHeight();

        void Clear();
    }
}
