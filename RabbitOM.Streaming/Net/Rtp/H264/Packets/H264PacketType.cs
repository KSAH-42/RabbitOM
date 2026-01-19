using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Packets
{
    /// <summary>
    /// Represent the H264 payload format type
    /// </summary>
    public enum H264PacketType
	{
        /// <summary>
        /// Unknown
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_SLICE = 1,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_DPA = 2,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_DPB = 3,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_DPC = 4,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_IDR = 5,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_SEI = 6,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_SPS = 7,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_PPS = 8,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_AUD = 9,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_EOSEQ = 10,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_EOSTREAM = 11,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_FILL = 12,

        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_A = 13,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_B = 14,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_C = 15,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_D = 16,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_E = 17,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_F = 18,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_G = 19,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_H = 20,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_I = 21,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_J = 22,
        
        /// <summary>
        /// A single nal
        /// </summary>
        SINGLE_RESERVED_K = 23,

        /// <summary>
        /// An aggregated nal
        /// </summary>
        AGGREGATION_STAP_A = 24,
        
        /// <summary>
        /// An aggregated nal
        /// </summary>
        AGGREGATION_STAP_B = 25,
        
        /// <summary>
        /// An aggregated nal
        /// </summary>
        AGGREGATION_MTAP_16 = 26,
        
        /// <summary>
        /// An aggregated nal
        /// </summary>
        AGGREGATION_MTAP_24 = 27,
        
        /// <summary>
        /// A fragmented nal
        /// </summary>
        FRAGMENTATION_FU_A = 28,
        
        /// <summary>
        /// A fragmented nal
        /// </summary>
        FRAGMENTATION_FU_B = 29,
    }
}