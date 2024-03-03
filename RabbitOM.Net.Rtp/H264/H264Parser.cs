/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED

  TryMethod :

        // Time complexity O(N) as first view

        // Should be Time complexity O(N,M) => see H264NalUnit.TryParse

        // TODO: try to improve again as O(N)

*/
using System;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264Parser
    {
        private readonly H264ParserConfiguration _configuration = new H264ParserConfiguration();

        public H264ParserConfiguration Configuration
        {
            get => _configuration;
        }

        public bool TryParse( RTPFrame frame , out H264NalUnitCollection result )
        {
            result = null;

            if ( frame == null || ! frame.TryValidate() )
            {
                return false;
            }

            H264NalUnitCollection nalunits = new H264NalUnitCollection();

            foreach ( var packet in frame.Packets )
            {
                if ( ! _configuration.IsPayloadSupported( packet.Type ) )
                {
                    continue;
                }

                if ( H264NalUnit.TryParse( packet.Data , out H264NalUnit nalUnit ) )
                {
                    nalunits.Enqueue( nalUnit );
                }
            }

            result = nalunits.IsEmpty ? null : nalunits;

            return result != null;
        }
    }
}