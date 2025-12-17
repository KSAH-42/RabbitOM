using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent a H265 frame
    /// </summary>
    public sealed class H265Frame : RtpFrame
    {
        /// <summary>
        /// Initialize an new instance of a H265 frame
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="pps">the pps</param>
        /// <param name="sps">the sps</param>
        /// <param name="vps">the vps</param>
        /// <param name="paramsBuffer">the params buffers</param>
        public H265Frame( byte[] data , byte[] pps , byte[] sps , byte[] vps , byte[] paramsBuffer ) : base ( data )
        {
            PPS = pps;
            SPS = sps;
            VPS = vps;
            ParamsBuffer = paramsBuffer;
        }

        /// <summary>
        /// Gets the PPS
        /// </summary>
        public byte[] PPS { get; }
        
        /// <summary>
        /// Gets the SPS
        /// </summary>
        public byte[] SPS { get; }

        /// <summary>
        /// Gets the VPS
        /// </summary>
        public byte[] VPS { get; }

        /// <summary>
        /// Gets the params buffer
        /// </summary>
        public byte[] ParamsBuffer { get; }
    }
}