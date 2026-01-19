using System;

namespace RabbitOM.Streaming.Net.Rtp.H266.Payloads
{
    public enum H266PayloadType
    {
        UNKNOWN = -1,

        TRAIL_N = 0,
        TRAIL_R = 1,

        TSA_N = 2,
        TSA_R = 3,

        STSA_N = 4,
        STSA_R = 5,

        RADL_N = 6,
        RADL_R = 7,
        RASL_N = 8,
        RASL_R = 9,

        RSVVCL_10 = 10,
        RSVVCL_11 = 11,
        RSVVCL_12 = 12,
        RSVVCL_13 = 13,
        RSVVCL_14 = 14,
        RSVVCL_15 = 15,

        VPS = 16,
        SPS = 17,
        PPS = 18,
        PREFIX_APS = 19,
        SUFFIX_APS = 20,

        PH = 21,
        AUD = 22,
        EOS = 23,
        EOB = 24,
        PREFIX_SEI = 25,
        SUFFIX_SEI = 26,

        RSVNVCL_27 = 27,
        RSVNVCL_28 = 28,
        RSVNVCL_29 = 29,
        RSVNVCL_30 = 30,
        RSVNVCL_31 = 31,
    }
}
