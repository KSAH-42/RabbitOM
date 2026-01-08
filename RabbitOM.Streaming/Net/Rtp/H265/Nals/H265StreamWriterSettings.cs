using System;

namespace RabbitOM.Streaming.Net.Rtp.H265.Nals
{
    /// <summary>
    /// Represent a H265 stream writer settings class
    /// </summary>
    public sealed class H265StreamWriterSettings
    {
        /// <summary>
        /// Gets / Sets the pps
        /// </summary>
        public byte[] PPS { get; set; }

        /// <summary>
        /// Gets / Sets the SPS
        /// </summary>
        public byte[] SPS { get; set; }

        /// <summary>
        /// Gets / Sets the vps
        /// </summary>
        public byte[] VPS { get; set; }

        /// <summary>
        /// Gets / Sets the DONL usage
        /// </summary>
        public bool DONL { get; set; }





        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryValidate()
        {
            return PPS?.Length > 0 && SPS?.Length > 0 && VPS?.Length > 0;
        }
        
        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            PPS = null;
            SPS = null;
            VPS = null;
            DONL = false;
        }
    }
}
