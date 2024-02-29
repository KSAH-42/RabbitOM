/*
 EXPERIMENTATION of the next implementation of the rtp layer


                    IMPLEMENTATION  NOT COMPLETED


*/

using System;

namespace RabbitOM.Net.Rtp
{
	public enum PayloadType : byte
	{
		Pcmu = 0,
		G721 = 2,
		Gsm = 3,
		G723 = 4,
		Dvi4_8000 = 5,
		Dvi4_16000 = 6,
		Lpc = 7,
		Pcma = 8,
		G722 = 9,
		L16_1Ch = 10,
		L16_2Ch = 11,
		Qcelp = 12,
		CN = 13,
		Mpa = 14,
		G728 = 0xF,
		Dvi4_11025 = 0x10,
		Dvi4_22050 = 17,
		G729 = 18,
		Celb = 25,
		Jpeg = 26,
		CustomType = 103,
		FrameInfo = 104,
		Nv = 28,
		H261 = 0x1F,
		Mpv = 0x20,
		Mpeg2Elementary = 0x20,
		Mp2T = 33,
		Mpeg2Program = 33,
		H263 = 34,
		Vcs = 34,
		VcsH264 = 35,
		RtcpReserved1 = 72,
		RtcpReserved2 = 73,
		RtcpReserved3 = 74,
		RtcpReserved4 = 75,
		RtcpReserved5 = 76,
		Message = 90,
		PcmSs = 91,
		PcmuSs = 92,
		Mpeg4 = 96,
		MessageVcs = 97,
		Mpeg4Dynamic2 = 97,
		Mpeg4Dynamic3 = 98,
		Mpeg4Dynamic4 = 99,
		Mpeg4Dynamic5 = 100,
	}
}