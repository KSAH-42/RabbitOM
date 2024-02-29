/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED
*/

using System;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264Parser
    {
        // Time complexity O(N) as first view

        // Should be Time complexity O(N,M) => see H264NalUnit.TryParse

        // TODO: try to improve again as O(N)

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
                if ( packet.PayloadType != (byte) PayloadType.Mpeg4 )
                {
                    continue;
                }

                if ( ! H264NalUnit.TryParse( packet.Data , out H264NalUnit nalUnit ) )
                {
                    continue;
                }

                if ( nalUnit.ForbiddenBit || nalUnit.IsUnDefinedNri )
                {
                    continue;
                }

                nalunits.Enqueue( nalUnit );
            }

            result = nalunits.IsEmpty ? null : nalunits;

            return result != null;
        }
    }
}