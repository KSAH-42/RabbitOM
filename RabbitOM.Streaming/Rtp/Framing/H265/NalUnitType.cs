using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
	public enum NalUnitType : byte
	{
		INVALID = -1,
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
		BLA_W_LP = 0X10,
		BLA_W_RADL = 17,
		BLA_N_LP = 18,
		IDR_W_RADL = 19,
		IDR_N_LP = 20,
		CRA = 21,
		VPS = 0X20,
		SPS = 33,
		PPS = 34,
		AUD = 35,
		EOS = 36,
		EOB = 37,
		FILLER = 38,
		PREFIXSEI = 39,
		SUFSEI = 40,
		AGGREGATE = 48,
		FRAGMENT = 49,
		PACI = 50
	}
}
