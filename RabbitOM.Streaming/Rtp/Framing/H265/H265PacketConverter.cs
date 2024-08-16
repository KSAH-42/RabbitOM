using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265PacketConverter
    {
        public bool ForceValidation { get; set; } = true;




        public H265NalUnit Convert( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( ! H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) )
            {
                throw new FormatException( nameof( packet ) );
            }

            if ( ForceValidation && ! nalUnit.TryValidate() )
            {
                throw new InvalidOperationException( nameof( packet ) );
            }

            return nalUnit;
        }
    } 
}