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
        /// <param name="sps">sps</param>
        /// <param name="pps">pps</param>
        public H264FrameBuilderConfiguration( byte[] sps , byte[] pps )
        {
            SPS = sps;
            PPS = pps;
        }

        /// <summary>
        /// Gets / Sets the sps
        /// </summary>
        public byte[] SPS { get; }

        /// <summary>
        /// Gets / Sets the PPS
        /// </summary>
        public byte[] PPS { get; }
    }
}
