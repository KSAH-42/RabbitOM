using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting
{
    public struct StringParameter
    {
        public StringParameter( string name , string value )
        {
            Name = name ?? string.Empty;
            Value = value ?? string.Empty;
        }

        public string Name { get; }

        public string Value { get; }
    }
}
