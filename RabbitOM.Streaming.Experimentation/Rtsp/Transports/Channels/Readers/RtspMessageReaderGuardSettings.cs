using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReaderGuardSettings
    {
        public RtspMessageReaderGuardSettings( long? headersCountLimit , long? totalHeaderLength , long? contentLengthLimit )
        {
            HeadersCountLimit = headersCountLimit;
            TotalHeaderLength = totalHeaderLength;
            ContentLengthLimit = contentLengthLimit;
        }

        public long? HeadersCountLimit { get; }

        public long? TotalHeaderLength { get; }

        public long? ContentLengthLimit { get; }
    }
}
