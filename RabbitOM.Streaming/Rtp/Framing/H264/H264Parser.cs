using System;

namespace RabbitOM.Streaming.Rtp.Framing.H264
{
    public sealed class H264Parser
    {
        private readonly H264ParserConfiguration _configuration = new H264ParserConfiguration();

        public H264ParserConfiguration Configuration
        {
            get => _configuration;
        }

        // Time  complexiy : O(N) => but probably O(N,M)

        // Space complexity : O(1) => but probably O(1,M)

        // Try to improve it

        // Parse not actually tested

        public bool TryParse( RTPFrame frame , out H264NalUnitCollection result )
        {
            result = null;

            if ( frame == null )
            {
                return false;
            }

            H264NalUnitCollection nalunits = new H264NalUnitCollection();

            foreach ( var packet in frame.Packets )
            {
                if ( ! _configuration.SupportsPayload( packet.Type ) )
                {
                    return false;
                }
                
                if ( H264NalUnit.TryParse( packet.Payload , out H264NalUnit nalUnit ) )
                {
                    nalunits.Enqueue( nalUnit );
                }
            }

            result = nalunits.IsEmpty ? null : nalunits;

            return result != null;
        }
    }
}