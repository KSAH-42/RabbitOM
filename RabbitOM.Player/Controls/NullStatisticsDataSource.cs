using System;

namespace RabbitOM.Player.Controls
{
    public sealed class NullStatisticsDataSource : IStatisticsDataSource
    {
        public static readonly NullStatisticsDataSource Value = new NullStatisticsDataSource();
        public string GetCodec() => string.Empty;
        public string GetTransport() => string.Empty;
        public long GetClock() => default;
        public bool GetConnectionStatus() => default;
        public long GetBytesReceivedPerSecond() => default;
        public long GetPacketReceivedPerSecond() => default;
        public long GetFrameCountPerSecond() => default;
        public long GetFrameHeight() => default;
        public long GetFrameWidth() => default;
        public long GetPacketsLostCount() => default;
        public long GetMaxFrameCountPerSecond() => default;
        public long GetMaxBytesReceivedPerSecond() => default;
        public long GetMaxPacketReceivedPerSecond() => default;
        public void Clear(){}
    }
}
