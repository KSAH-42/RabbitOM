using System;

namespace RabbitOM.Streaming.Net.Rtp.Framing.H265
{
    public sealed class H265PacketConverter
    {
        public bool UseNalUnitValidation { get; set; } = true;

        
        public bool TryConvert( RtpPacket packet , out H265NalUnit result )
        {
            result = null;

            if ( packet == null )
            {
                return false;
            }

            if ( ! H265NalUnit.TryParse( packet.Payload , out result ) )
            {
                return false;
            }

            return UseNalUnitValidation ? result.TryValidate() : true;
        }
    }
}