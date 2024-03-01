/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED
*/
using System;

namespace RabbitOM.Net.Rtp.H265
{
    public sealed class H265Parser
    {
        // Time complexity O(N) as first view

        // It should be O(N,M) => see frame.TryValidate & H264NalUnit.TryParse methods

        // TODO: try improvement on space complexity, espacially on H264NalUnit.TryParse

        public bool TryParse( RTPFrame frame , out H265NalUnitCollection result )
        {
            result = null;

            if ( frame == null || ! frame.TryValidate() )
            {
                return false;
            }

            H265NalUnitCollection nalunits = new H265NalUnitCollection();

            foreach ( var packet in frame.Packets )
            {
                if ( H265NalUnit.TryParse( packet.Data , out H265NalUnit nalUnit ) )
                {
                    nalunits.Enqueue( nalUnit );
                }
            }

            result = nalunits.IsEmpty ? null : nalunits;

            return result != null;
        }
    }
}