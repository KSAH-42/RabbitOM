/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

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
                return false;

            H264NalUnitCollection nalunits = new H264NalUnitCollection();

            foreach ( var packet in frame.Packets )
            {
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