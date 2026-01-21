using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp
{
    public sealed class DefaultRtpPacketInspector : RtpPacketInspector
    {
        public const int DefaultMTU = 1500;

        public const int DefaultMaximumOfPacketsSize = DefaultMTU * 4;




        private readonly HashSet<RtpPacketType> _packetsTypes = new HashSet<RtpPacketType>();




        public int? MinimumPayloadSize { get; set; } 

        public int? MaximumPayloadSize { get; set; } = DefaultMaximumOfPacketsSize;

        public uint? RecognizedSSRC { get; set; }

        public ISet<RtpPacketType> PacketsTypes
        {
            get => _packetsTypes;
        }

        



        public override void Inspect( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( ! packet.TryValidate() )
            {
                throw new ArgumentException( nameof( packet ) , "the packet seems to be incorrect" );
            }

            if ( MinimumPayloadSize.HasValue && MinimumPayloadSize > packet.Payload.Count )
            {
                throw new InvalidOperationException( $"UnAuthorize packet: invalid payload minimum size {packet.Payload.Count}" );
            }

            if ( MaximumPayloadSize.HasValue && MaximumPayloadSize < packet.Payload.Count )
            {
                throw new InvalidOperationException( $"UnAuthorize packet: invalid payload maximum size {packet.Payload.Count}" );
            }

            if ( RecognizedSSRC.HasValue && RecognizedSSRC != packet.SSRC )
            {
                throw new InvalidOperationException( $"UnAuthorize packet: unrecognized ssrc size {packet.SSRC}" );
            }

            if ( _packetsTypes.Count > 0 && ! _packetsTypes.Contains( packet.Type ) )
            {
                throw new InvalidOperationException( $"UnAuthorize packet : invalid type {packet.Type}" );
            }
        }
    }
}
