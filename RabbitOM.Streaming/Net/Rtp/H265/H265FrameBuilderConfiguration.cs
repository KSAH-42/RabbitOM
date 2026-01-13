using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent a H265 settings class
    /// </summary>
    public sealed class H265FrameBuilderConfiguration
    {
        /// <summary>
        /// Initialize a new instance of settings class
        /// </summary>
        /// <param name="vps">vps</param>
        /// <param name="sps">sps</param>
        /// <param name="pps">pps</param>
        public H265FrameBuilderConfiguration( byte[] vps , byte[] sps , byte[] pps )
            : this ( vps , sps , pps , false )
        {
        }

        /// <summary>
        /// Initialize a new instance of settings class
        /// </summary>
        /// <param name="vps">vps</param>
        /// <param name="sps">sps</param>
        /// <param name="pps">pps</param>
        /// <param name="donl">donl</param>
        public H265FrameBuilderConfiguration( byte[] vps , byte[] sps , byte[] pps , bool donl )
        {
            VPS = vps;
            SPS = sps;
            PPS = pps;
            DONL = donl;
        }

        /// <summary>
        /// Gets / Sets the vps
        /// </summary>
        public byte[] VPS { get; }

        /// <summary>
        /// Gets / Sets the SPS
        /// </summary>
        public byte[] SPS { get; }

        /// <summary>
        /// Gets / Sets the pps
        /// </summary>
        public byte[] PPS { get; }

        /// <summary>
        /// Gets / Sets the DONL usage
        /// </summary>
        public bool DONL { get; }
    }
}
