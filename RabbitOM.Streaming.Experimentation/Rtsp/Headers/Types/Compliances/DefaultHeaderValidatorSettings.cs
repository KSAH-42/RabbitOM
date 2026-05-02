using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public sealed class DefaultHeaderValidatorSettings
    {
        public bool AcceptEmptyOrWhiteSpace { get; set; }

        public Func<string,bool> OnValidate { get; set; }
    }
}
