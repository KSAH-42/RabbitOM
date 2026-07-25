using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels.Readers
{
    public sealed class RtspMessageReaderValidatorOptions
    {
        public RtspMessageReaderValidatorOptions( int maximumOfHeaders , int maximumOfHeadersTotalLength , int maximumOfHeaderLength , int maximumOfContentLengthValue )
        {
            MaximumOfHeaders = maximumOfHeaders;
            MaximumOfHeadersTotalLength = maximumOfHeadersTotalLength;
            MaximumOfHeaderLength = maximumOfHeaderLength;
            MaximumOfContentLengthValue = maximumOfContentLengthValue;
        }

        public int? MaximumOfHeaders { get; }

        public int? MaximumOfHeadersTotalLength { get; }

        public int? MaximumOfHeaderLength { get; }

        public int? MaximumOfContentLengthValue { get; }
    }
}
