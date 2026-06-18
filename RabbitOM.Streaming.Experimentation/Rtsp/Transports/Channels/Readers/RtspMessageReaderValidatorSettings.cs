using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReaderValidatorSettings
    {
        public RtspMessageReaderValidatorSettings( long? headersCountLimit , long? totalHeadersSize , long? contentLengthLimit )
        {
            HeadersCountLimit = headersCountLimit;
            TotalHeadersSizeLimit = totalHeadersSize;
            ContentLengthLimit = contentLengthLimit;
        }

        public long? HeadersCountLimit { get; }

        public long? TotalHeadersSizeLimit { get; }

        public long? ContentLengthLimit { get; }
    }
}
