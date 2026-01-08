using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    /// <summary>
    /// Represent a report bloc
    /// </summary>
    public sealed class RtcpReportBlock
    {
        /// <summary>
        /// The constant size
        /// </summary>
        public const int Size = 24;






        /// <summary>
        /// Initialize a new instance of the report block class
        /// </summary>
        private RtcpReportBlock()
        {
        }

        /// <summary>
        /// Initialize a new instance of the report block class
        /// </summary>
        /// <param name="synchronizationSource">the source id</param>
        /// <param name="fractionLost">the fraction lost</param>
        /// <param name="cummulativePacketsLost">the number of packets lost</param>
        /// <param name="extendedHighestSequence">the higher extension sequence</param>
        /// <param name="interArrivalJitter">the jitter info</param>
        /// <param name="lastSRTimestamp">the last sender report timestamp</param>
        /// <param name="delaySinceLastSR">the delay of last sender report</param>
        public RtcpReportBlock( uint synchronizationSource , byte fractionLost , uint cummulativePacketsLost , uint extendedHighestSequence , uint interArrivalJitter , uint lastSRTimestamp , uint delaySinceLastSR )
        {
            SynchronizationSource = synchronizationSource;
            FractionLost = fractionLost;
            CummulativePacketsLost = cummulativePacketsLost;
            ExtendedHighestSequence = extendedHighestSequence;
            InterArrivalJitter = interArrivalJitter;
            LastSRTimestamp = lastSRTimestamp;
            DelaySinceLastSR = delaySinceLastSR;
        }
        





        /// <summary>
        /// Gets the source id
        /// </summary>
        public uint SynchronizationSource { get; private set; }
        
        /// <summary>
        /// Gets the fraction lost
        /// </summary>
        public byte FractionLost { get; private set; }
        
        /// <summary>
        /// Gets the cummulative packets lost
        /// </summary>
        public uint CummulativePacketsLost { get; private set; }
        
        /// <summary>
        /// Gets the extended highest sequence
        /// </summary>
        public uint ExtendedHighestSequence { get; private set; }
        
        /// <summary>
        /// Gets the inter arrival jitter info
        /// </summary>
        public uint InterArrivalJitter { get; private set; }
        
        /// <summary>
        /// Gets the last SR timestamp
        /// </summary>
        public uint LastSRTimestamp { get; private set; }
        
        /// <summary>
        /// Gets the delay since the last SR
        /// </summary>
        public uint DelaySinceLastSR { get; private set; }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( in ArraySegment<byte> buffer , out RtcpReportBlock result )
        {
            result = default;

            if ( buffer.Array == null || buffer.Count < Size )
            {
                return false;
            }
            
            var offset = buffer.Offset;
            
            result.SynchronizationSource  = (uint) buffer.Array[ offset ++ ] << 24;
            result.SynchronizationSource |= (uint) buffer.Array[ offset ++ ] << 16;
            result.SynchronizationSource |= (uint) buffer.Array[ offset ++ ] << 8;
            result.SynchronizationSource |=        buffer.Array[ offset ++ ];

            result.FractionLost = buffer.Array[ offset ++ ];

            result.CummulativePacketsLost  = (uint) buffer.Array[ offset ++ ] << 16;
            result.CummulativePacketsLost |= (uint) buffer.Array[ offset ++ ] << 8;
            result.CummulativePacketsLost |=        buffer.Array[ offset ++ ];

            result.ExtendedHighestSequence  = (uint) buffer.Array[ offset ++ ] << 24;
            result.ExtendedHighestSequence |= (uint) buffer.Array[ offset ++ ] << 16;
            result.ExtendedHighestSequence |= (uint) buffer.Array[ offset ++ ] << 8;
            result.ExtendedHighestSequence |=        buffer.Array[ offset ++ ];

            result.InterArrivalJitter  = (uint) buffer.Array[ offset ++ ] << 24;
            result.InterArrivalJitter |= (uint) buffer.Array[ offset ++ ] << 16;
            result.InterArrivalJitter |= (uint) buffer.Array[ offset ++ ] << 8;
            result.InterArrivalJitter |=        buffer.Array[ offset ++ ];

            result.LastSRTimestamp  = (uint) buffer.Array[ offset ++ ] << 24;
            result.LastSRTimestamp |= (uint) buffer.Array[ offset ++ ] << 16;
            result.LastSRTimestamp |= (uint) buffer.Array[ offset ++ ] << 8;
            result.LastSRTimestamp |=        buffer.Array[ offset ++ ];

            result.DelaySinceLastSR  = (uint) buffer.Array[ offset ++ ] << 24;
            result.DelaySinceLastSR |= (uint) buffer.Array[ offset ++ ] << 16;
            result.DelaySinceLastSR |= (uint) buffer.Array[ offset ++ ] << 8;
            result.DelaySinceLastSR |=        buffer.Array[ offset ++ ];

            return true;
        }
    }
}
