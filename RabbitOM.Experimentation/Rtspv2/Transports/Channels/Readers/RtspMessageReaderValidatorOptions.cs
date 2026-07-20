using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels.Readers
{
    public sealed class RtspMessageReaderValidatorOptions
    {
        public int? MaximumOfHeaders { get; set; }

        public int? MaximumOfHeadersTotalLength { get; set; }

        public int? MaximumOfHeaderLength { get; set; }

        public int? MaximumOfContentLengthValue { get; set; }
    }
}
