using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Headers
{
    public enum NalUnitType
	{
        UNKNOWN = 0,
        SLICE = 1,
        DPA = 2,
        DPB = 3,
        DPC = 4,
        IDR = 5,
        SEI = 6,
        SPS = 7,
        PPS = 8,
        AUD = 9,
        EOSEQ = 10,
        EOSTREAM = 11,
        FILL = 12,
    }
}
