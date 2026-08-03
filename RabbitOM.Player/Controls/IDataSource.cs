using System;

namespace RabbitOM.Player.Controls
{
    public interface IDataSource
    {
        string Codec { get; }

        bool ConnectionStatus{ get; }

        long ConnectionsCount{ get; }

        long BytesReceivedPerSecond { get; }

        long PacketReceivedPerSecond { get; }

        long FrameCountPerSecond { get; }

        void Clear();
    }
}
