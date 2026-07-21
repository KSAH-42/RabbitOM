using System;

namespace RabbitOM.Streaming.RtspV2.Authentication
{
    // Apply the output of this class to <seealso cref="RtspAuthorizationResponseBuilder.NonceCount"/> property
    public sealed class NonceCounter
    {
        public uint Value { get; set; }

        public void Increment() => Value ++;

        public override string ToString() => Value.ToString( "X8" );
    }
}
