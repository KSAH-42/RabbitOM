namespace RabbitOM.Decoder.FFMpeg.Channels
{
	public interface IVideoChannelMetrics
	{
		string? GetCodec();

        string? GetTransport();

        bool GetConnectionStatus();

        long GetClock();

        long GetBytesReceivedPerSecond();

        long GetPacketReceivedPerSecond();

        long GetFrameCountPerSecond();

        long GetFrameWidth();

        long GetFrameHeight();

        long GetPacketsLostCount();

        long GetMaxFrameCountPerSecond();

        long GetMaxBytesReceivedPerSecond();

        long GetMaxPacketReceivedPerSecond();

        void Clear();
	}
}
