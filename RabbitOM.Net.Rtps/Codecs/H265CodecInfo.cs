using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp
{
	/// <summary>
	/// Represent a video codec
	/// </summary>
	public sealed class H265CodecInfo : VideoCodecInfo
	{
		private readonly byte[] _sps_pps;

		private readonly byte[] _vps;

		/// <summary>
		/// Gets the codec type
		/// </summary>
		public override CodecType Type => CodecType.H265;

		/// <summary>
		/// Gets the sequence parameter and picture parameters values
		/// </summary>
		public byte[] SPS_PPS => _sps_pps;

		/// <summary>
		/// Gets the video parameter sets
		/// </summary>
		public byte[] VPS => _vps;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sps_pps">the sequence and picture parameter set</param>
		public H265CodecInfo(byte[] sps_pps)
			: this(sps_pps, null)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sps_pps">the sequence and picture parameter set</param>
		/// <param name="vps">the video parameter set</param>
		public H265CodecInfo(byte[] sps_pps, byte[] vps)
		{
			_sps_pps = (sps_pps ?? new byte[0]);
			_vps = (vps ?? new byte[0]);
		}

		/// <summary>
		/// Validate
		/// </summary>
		/// <returns>returns true</returns>
		public override bool TryValidate()
		{
			if (_sps_pps == null || _sps_pps.Length == 0)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Create a codec descriptor
		/// </summary>
		/// <returns>returns an instance, otherwise null</returns>
		public static H265CodecInfo Create()
		{
			return Create("QgEBAWAAAAMAsAAAAwAAAwB7oAPAgBDlja5JMvwCAAADAAIAAAMAZUI=", "RAHA8vA8kAA=", string.Empty);
		}

		/// <summary>
		/// Create a codec descriptor
		/// </summary>
		/// <param name="sps">the sps value</param>
		/// <param name="pps">the pps value</param>
		/// <param name="vps">the vps</param>
		/// <returns>returns an instance, otherwise null</returns>
		public static H265CodecInfo Create(string sps, string pps, string vps)
		{
			if (string.IsNullOrWhiteSpace(sps) || string.IsNullOrWhiteSpace(pps))
			{
				return null;
			}

			try
			{
				byte[] spsBuffer = Convert.FromBase64String(sps.Trim());

				if (spsBuffer == null || spsBuffer.Length == 0)
				{
					return null;
				}

				IEnumerable<byte> spsBytes = CodecInfo.H265StartMarker.Concat(spsBuffer);
				
				if (spsBytes == null)
				{
					return null;
				}
				
				byte[] ppsBuffer = Convert.FromBase64String(pps.Trim());
				
				if (ppsBuffer == null || ppsBuffer.Length == 0)
				{
					return null;
				}
				
				IEnumerable<byte> ppsBytes = CodecInfo.H265StartMarker.Concat(ppsBuffer);
				
				if (ppsBytes == null)
				{
					return null;
				}
				
				return new H265CodecInfo(spsBytes.Concat(ppsBytes).ToArray(), Convert.FromBase64String(vps));
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine( ex );
			}

			return null;
		}
	}
}