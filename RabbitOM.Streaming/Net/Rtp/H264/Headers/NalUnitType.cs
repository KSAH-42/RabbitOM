using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Headers
{
    public enum NalUnitType
	{
        UNKNOWN = 0,

        SINGLE_SLICE = 1,
        SINGLE_DPA = 2,
        SINGLE_DPB = 3,
        SINGLE_DPC = 4,
        SINGLE_IDR = 5,
        SINGLE_SEI = 6,
        SINGLE_SPS = 7,
        SINGLE_PPS = 8,
        SINGLE_AUD = 9,
        SINGLE_EOSEQ = 10,
        SINGLE_EOSTREAM = 11,
        SINGLE_FILL = 12,

        SINGLE_RESERVED_A = 13,
        SINGLE_RESERVED_B = 14,
        SINGLE_RESERVED_C = 15,
        SINGLE_RESERVED_D = 16,
        SINGLE_RESERVED_E = 17,
        SINGLE_RESERVED_F = 18,
        SINGLE_RESERVED_G = 19,
        SINGLE_RESERVED_H = 20,
        SINGLE_RESERVED_I = 21,
        SINGLE_RESERVED_J = 22,
        SINGLE_RESERVED_K = 23,

        AGGREGATION_STAP_A = 24,
        AGGREGATION_STAP_B = 25,
        AGGREGATION_MTAP_16 = 26,
        AGGREGATION_MTAP_24 = 27,
        FRAGMENTATION_FU_A = 28,
        FRAGMENTATION_FU_B = 29,
    }
}
