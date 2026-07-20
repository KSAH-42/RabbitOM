using System;

namespace RabbitOM.Streaming.Rtp
{
    public sealed class RtpStartCodePrefix
    {
        private RtpStartCodePrefix( byte[] value ) => Value = value;

        public byte[] Value { get; }

        public static RtpStartCodePrefix ThreeBytes { get; } = new RtpStartCodePrefix( new byte[] { 0x00 , 0x00 , 0x01 } );

        public static RtpStartCodePrefix FourBytes { get; } = new RtpStartCodePrefix( new byte[] { 0x00 , 0x00 , 0x00 , 0x01 } );
    }
}
