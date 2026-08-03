using System;

namespace RabbitOM.Player.Controls
{
    public interface IDataSource
    {
        string GetCodec();

        bool GetConnectionStatus();

        long GetBytesReceivedPerSecond();

        long GetPacketReceivedPerSecond();

        long GetFrameCountPerSecond();

        void Clear();
    }
}
