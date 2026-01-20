using System;

namespace RabbitOM.Streaming.Net.Rtp.H266.Payloads
{
    public enum H266PayloadType : byte
    {
         // VCL types
         TRAIL       = 0,
         STSA        = 1,
         RADL        = 2,
         RASL        = 3,
         RSV_VCL_4   = 4,
         RSV_VCL_5   = 5,
         RSV_VCL_6   = 6,
         IDR_W_RADL  = 7,
         IDR_N_LP    = 8,
         CRA_NUT     = 9,
         GDR_NUT     = 10,
         RSV_IRAP_11 = 11,

         // None VCL types
         OPI         = 12,
         DCI         = 13,
         VPS         = 14,
         SPS         = 15,
         PPS         = 16,
         PREFIX_APS  = 17,
         SUFFIX_APS  = 18,
         PH          = 19,
         AUD         = 20,
         EOS_NUT     = 21,
         EOB_NUT     = 22,
         PREFIX_SEI  = 23,
         SUFFIX_SEI  = 24,
         FD_NUT      = 25,
         RSV_NVCL_26 = 26,
         RSV_NVCL_27 = 27,
         RSV_NVCL_28 = 28,
         RSV_NVCL_29 = 29,
         RSV_NVCL_30 = 30,
         RSV_NVCL_31 = 31,
    }
}
