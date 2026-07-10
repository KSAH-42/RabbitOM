using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Authentication
{
    public sealed class NonceCounter
    {
        public uint Value { get; set; }

        public void Increment() => Value ++;

        public override string ToString() => Value.ToString( "X8" );
    }
}
