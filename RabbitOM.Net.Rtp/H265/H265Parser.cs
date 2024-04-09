using System;

namespace RabbitOM.Net.Rtp.H265
{
    public sealed class H265Parser
    {
        private readonly H265ParserConfiguration _configuration = new H265ParserConfiguration();

        public H265ParserConfiguration Configuration
        {
            get => _configuration;
        }

        public bool TryParse( RTPFrame frame , out H265NalUnitCollection result )
        {
            result = null;

            if ( frame == null )
            {
                return false;
            }

            H265NalUnitCollection nalunits = new H265NalUnitCollection();

            foreach ( var packet in frame.Packets )
            {
                if ( ! _configuration.SupportsPayload( packet.Type ) )
                {
                    continue;
                }

                if ( H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalUnit ) )
                {
                    nalunits.Enqueue( nalUnit );
                }
            }

            result = nalunits.IsEmpty ? null : nalunits;

            return result != null;
        }
    }
}