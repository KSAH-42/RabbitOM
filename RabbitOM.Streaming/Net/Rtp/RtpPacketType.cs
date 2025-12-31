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
		PCMU = 0,

        /// <summary>
        /// FS_1016
        /// </summary>
        FS_1016 = 1,

        /// <summary>
        /// G721_726
        /// </summary>
        G721_726 = 2,

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
        PCMA = 8,

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
        /// MPEG4
        /// </summary>
        MPEG4 = 96,

        /// <summary>
        /// MPEG4_DYNAMIC_A
        /// </summary>
        MPEG4_DYNAMIC_A = 97,

        /// <summary>
        /// MPEG4_DYNAMIC_B
        /// </summary>
        MPEG4_DYNAMIC_B = 98,

        /// <summary>
        /// MPEG4_DYNAMIC_C
        /// </summary>
        MPEG4_DYNAMIC_C = 99,

        /// <summary>
        /// MPEG4_DYNAMIC_D
        /// </summary>
        MPEG4_DYNAMIC_D = 100,

        /// <summary>
        /// METADATA
        /// </summary>
        METADATA = 126,
    }
}
