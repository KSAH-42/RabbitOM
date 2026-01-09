using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    /// <summary>
    /// Represent a H264 settings class
    /// </summary>
    public sealed class H264FrameBuilderConfiguration
    {
        /// <summary>
        /// Initialize a new instance of settings class
        /// </summary>
        /// <param name="pps">pps</param>
        /// <param name="sps">sps</param>
        public H264FrameBuilderConfiguration( byte[] pps , byte[] sps )
        {
            PPS = pps;
            SPS = sps;
        }

        /// <summary>
        /// Gets / Sets the pps
        /// </summary>
        public byte[] PPS { get; }

        /// <summary>
        /// Gets / Sets the SPS
        /// </summary>
        public byte[] SPS { get; }
    }
}
