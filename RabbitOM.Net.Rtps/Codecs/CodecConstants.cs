using System;

namespace RabbitOM.Net.Rtps.Codecs
{
    /// <summary>
    /// Represent an constants
    /// </summary>
    public static class CodecConstants
    {
        /// <summary>
        /// Represent the start marker vector
        /// </summary>
        public static readonly byte[] H264StartMarker = { 0 , 0 , 0 , 1 };

        /// <summary>
        /// Represent the default SPS for H264 encoder
        /// </summary>
        public const string Default_H264_SPS = "Z00AH5pkAoAt/4C1AQEBQAAA+gAAJxAh";

        /// <summary>
        /// Represent the default PPS for H264 encoder
        /// </summary>
        public const string Default_H264_PPS = "aO48gA==";

        /// <summary>
	    /// Represent the start marker vector
	    /// </summary>
	    public static readonly byte[] H265StartMarker = { 0, 0, 0, 1 };

        /// <summary>
        /// Represent the default SPS for H265 encoder
        /// </summary>
        public const string Default_H265_SPS = "QgEBAWAAAAMAsAAAAwAAAwB7oAPAgBDlja5JMvwCAAADAAIAAAMAZUI=";

	    /// <summary>
	    /// Represent the default PPS for H265 encoder
	    /// </summary>
	    public const string Default_H265_PPS = "RAHA8vA8kAA=";
    }
}
