﻿using System;

namespace RabbitOM.Streaming.Rtp
{
	public enum PacketType : byte
	{
		MINIMUM = 0,
		PCMU = 0,
		G721 = 2,
		GSM = 3,
		G723 = 4,
		DVI4_8000 = 5,
		DVI4_16000 = 6,
		LPC = 7,
		PCMA = 8,
		G722 = 9,
		L16_1CH = 10,
		L16_2CH = 11,
		QCELP = 12,
		CN = 13,
		MPA = 14,
		G728 = 0XF,
		DVI4_11025 = 0X10,
		DVI4_22050 = 17,
		G729 = 18,
		CELB = 25,
		JPEG = 26,
		NV = 28,
		H261 = 0X1F,
		MPV = 0X20,
		MPEG2_ELEMENTARY = 0X20,
		MP2T = 33,
		MPEG2_PROGRAM = 33,
		H263 = 34,
		VCS = 34,
		VCS_H264 = 35,
		RTCP_RESERVED1 = 72,
		RTCP_RESERVED2 = 73,
		RTCP_RESERVED3 = 74,
		RTCP_RESERVED4 = 75,
		RTCP_RESERVED5 = 76,
		PCMSS = 91,
		PCMUSS = 92,
		MPEG4 = 96,
		MPEG4_DYNAMIC2 = 97,
		MPEG4_DYNAMIC3 = 98,
		MPEG4_DYNAMIC4 = 99,
		MPEG4_DYNAMIC5 = 100,
		METADATA = 126,
		MAXIMUM = 127,
	}
}
