using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
	/// <summary>
	/// Represent the nal unit type
	/// </summary>
	public enum NalUnitType : byte
	{
		/// <summary>
		/// Coded slice segment of a non-TSA, non-STSA trailing picture (Non-IDR slice of a non-TSA, non-STSA picture).
		/// </summary>
		TRAIL_N = 0 ,

		/// <summary>
		/// Coded slice segment of a non-TSA, non-STSA trailing picture (IDR slice of a non-TSA, non-STSA picture).
		/// </summary>
		TRAIL_R = 1 ,

		/// <summary>
		/// Coded slice segment of a TSA (Temporal Sub-layer Access) picture.
		/// </summary>
		TSA_N = 2 ,

		/// <summary>
		/// Coded slice segment of a TSA (Temporal Sub-layer Access) picture with RADL (Referenceable Arrival Decoding).
		/// </summary>
		TSA_R = 3 ,

		/// <summary>
		/// Coded slice segment of an STSA (Step-wise Temporal Sub-layer Access) picture.
		/// </summary>
		STSA_N = 4 ,

		/// <summary>
		/// Coded slice segment of an STSA (Step-wise Temporal Sub-layer Access) picture with RADL.
		/// </summary>
		STSA_R = 5 ,

		/// <summary>
		/// Coded slice segment of a RADL (Referenceable Arrival Decoding) picture.
		/// </summary>
		RADL_N = 6 ,

		/// <summary>
		/// Coded slice segment of a RADL (Referenceable Arrival Decoding) picture with RADL.
		/// </summary>
		RADL_R = 7 ,

		/// <summary>
		/// Coded slice segment of a RASL (Referenceable Arrival Decoding) picture.
		/// </summary>
		RASL_N = 8 ,

		/// <summary>
		/// Coded slice segment of a RASL (Referenceable Arrival Decoding) picture with RADL.
		/// </summary>
		RASL_R = 9 ,

		/// <summary>
		/// Reserved non-IRAP (Instantaneous Refresh Access Point) NAL unit types.
		/// </summary>
		RSV_VCL_N10 = 10 ,

		/// <summary>
		/// Reserved IRAP NAL unit types.
		/// </summary>
		RSV_VCL_R11 = 11 ,

		/// <summary>
		/// Reserved non-IRAP NAL unit types.
		/// </summary>
		RSV_VCL_N12 = 12 ,

		/// <summary>
		/// Reserved IRAP NAL unit types.
		/// </summary>
		RSV_VCL_R13 = 13 ,

		/// <summary>
		/// Reserved non-IRAP NAL unit types.
		/// </summary>
		RSV_VCL_N14 = 14 ,

		/// <summary>
		/// Reserved IRAP NAL unit types.
		/// </summary>
		RSV_VCL_R15 = 15 ,

		/// <summary>
		/// Coded slice segment of a BLA (Broken Link Access) picture with leading picture.
		/// </summary>
		BLA_W_LP = 16 ,

		/// <summary>
		/// Coded slice segment of a BLA (Broken Link Access) picture with RADL.
		/// </summary>
		BLA_W_RADL = 17 ,

		/// <summary>
		/// Coded slice segment of a BLA (Broken Link Access) picture with no leading picture.
		/// </summary>
		BLA_N_LP = 18 ,

		/// <summary>
		/// Coded slice segment of an IDR (Instantaneous Decoding Refresh) picture with RADL.
		/// </summary>
		IDR_W_RADL = 19 ,

		/// <summary>
		/// Coded slice segment of an IDR (Instantaneous Decoding Refresh) picture with no leading picture.
		/// </summary>
		IDR_N_LP = 20 ,

		/// <summary>
		/// Coded slice segment of a CRA (Clean Random Access) picture.
		/// </summary>
		CRA_NUT = 21 ,

		/// <summary>
		/// Reserved IRAP NAL unit types.
		/// </summary>
		RSV_IRAP_VCL22 = 22 ,

		/// <summary>
		/// Reserved IRAP NAL unit types.
		/// </summary>
		RSV_IRAP_VCL23 = 23 ,

		/// <summary>
		/// Video parameter set.
		/// </summary>
		VPS_NUT = 24 ,

		/// <summary>
		/// Sequence parameter set.
		/// </summary>
		SPS_NUT = 25 ,

		/// <summary>
		/// Picture parameter set.
		/// </summary>
		PPS_NUT = 26 ,

		/// <summary>
		/// Access unit delimiter.
		/// </summary>
		AUD_NUT = 27 ,

		/// <summary>
		/// End of sequence.
		/// </summary>
		EOS_NUT = 28 ,

		/// <summary>
		/// End of bitstream.
		/// </summary>
		EOB_NUT = 29 ,

		/// <summary>
		/// Filler data.
		/// </summary>
		FD_NUT = 30 ,

		/// <summary>
		/// Supplemental enhancement information (SEI).
		/// </summary>
		PREFIX_SEI_NUT = 31 ,

		/// <summary>
		/// Supplemental enhancement information (SEI).
		/// </summary>
		SUFFIX_SEI_NUT = 32 ,
	}
}
