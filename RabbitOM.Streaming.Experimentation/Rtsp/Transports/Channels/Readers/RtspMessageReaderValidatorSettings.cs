using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReaderValidatorSettings
    {
        public RtspMessageReaderValidatorSettings( long? maximumOfHeaders , long? totalHeadersSize , long? contentLengthLimit )
        {
            MaximumOfHeaders = maximumOfHeaders;
            TotalHeadersSize = totalHeadersSize;
            ContentLengthLimit = contentLengthLimit;
        }

        public long? MaximumOfHeaders { get; }

        public long? TotalHeadersSize { get; }

        public long? ContentLengthLimit { get; }
    }
}
