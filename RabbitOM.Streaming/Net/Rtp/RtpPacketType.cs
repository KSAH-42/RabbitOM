using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    /// <summary>
    /// Represent the packet type
    /// </summary>
    public enum RtpPacketType : byte
    {
        /// <summary>
        /// PCMU
        /// </summary>
		PCM_U = 0,

        /// <summary>
        /// FS_1016
        /// </summary>
        FS_1016 = 1,

        /// <summary>
        /// G721_726
        /// </summary>
        G726 = 2,

        /// <summary>
        /// GSM
        /// </summary>
        GSM = 3,

        /// <summary>
        /// G723
        /// </summary>
        G723 = 4,

        /// <summary>
        /// DVI4_A
        /// </summary>
        DVI4_A = 5,

        /// <summary>
        /// DVI4_B
        /// </summary>
        DVI4_B = 6,

        /// <summary>
        /// LPC
        /// </summary>
        LPC = 7,

        /// <summary>
        /// PCMA
        /// </summary>
        PCM_A = 8,

        /// <summary>
        /// G722
        /// </summary>
        G722 = 9,

        /// <summary>
        /// L16_A
        /// </summary>
        L16_A = 10,

        /// <summary>
        /// L16_B
        /// </summary>
        L16_B = 11,

        /// <summary>
        /// QCELP
        /// </summary>
        QCELP = 12,

        /// <summary>
        /// CN
        /// </summary>
        CN = 13,

        /// <summary>
        /// MPA
        /// </summary>
        MPA = 14,

        /// <summary>
        /// G728
        /// </summary>
        G728 = 15,

        /// <summary>
        /// DVI4_C
        /// </summary>
        DVI4_C = 16,

        /// <summary>
        /// DVI4_D
        /// </summary>
        DVI4_D = 17,

        /// <summary>
        /// G729
        /// </summary>
        G729 = 18,

        /// <summary>
        /// CELB
        /// </summary>
        CELB = 25,

        /// <summary>
        /// JPEG
        /// </summary>
        JPEG = 26,

        /// <summary>
        /// NV
        /// </summary>
        NV = 28,

        /// <summary>
        /// H261
        /// </summary>
        H261 = 31,

        /// <summary>
        /// MPV
        /// </summary>
        MPV = 32,

        /// <summary>
        /// MP2T
        /// </summary>
        MP2T = 33,

        /// <summary>
        /// H263
        /// </summary>
        H263 = 34,

        /// <summary>
        /// RTCP_RESERVED_A 
        /// </summary>
        RTCP_RESERVED_A = 72,

        /// <summary>
        /// RTCP_RESERVED_B
        /// </summary>
        RTCP_RESERVED_B = 73,

        /// <summary>
        /// RTCP_RESERVED_C
        /// </summary>
        RTCP_RESERVED_C = 74,

        /// <summary>
        /// RTCP_RESERVED_D
        /// </summary>
        RTCP_RESERVED_D = 75,

        /// <summary>
        /// RTCP_RESERVED_E
        /// </summary>
        RTCP_RESERVED_E = 76,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_1 = 96,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_2 = 97,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_3 = 98,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_4 = 99,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_5 = 100,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_6 = 101,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_7 = 102,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_8 = 103,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_9 = 104,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_10 = 105,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_11 = 106,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_12 = 107,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_13 = 108,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_14 = 109,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_15 = 110,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_16 = 111,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_17 = 112,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_18 = 113,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_19 = 114,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_20 = 115,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_21 = 116,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_22 = 117,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_23 = 118,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_24 = 119,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_25 = 120,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_26 = 121,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_27 = 122,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_28 = 123,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_29 = 124,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_30 = 125,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_31 = 126,

        /// <summary>
        /// DYNAMIC TYPE
        /// </summary>
        DYNAMIC_32 = 127,
    }
}
