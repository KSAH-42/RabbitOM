using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReaderValidatorSettings
    {
        public RtspMessageReaderValidatorSettings()
        {
        }

        public RtspMessageReaderValidatorSettings( long? limitOfHeadersCount , long? limitOfCumulatedHeadersSize , long? limitOfContentLength )
        {
            LimitOfHeadersCount = limitOfHeadersCount;
            LimitOfCumulatedHeadersSize = limitOfCumulatedHeadersSize;
            LimitOfContentLength = limitOfContentLength;
        }

        public long? LimitOfHeadersCount { get; }

        public long? LimitOfCumulatedHeadersSize { get; }

        public long? LimitOfContentLength { get; }
    }
}
