using System;

namespace RabbitOM.Streaming.Net.Rtp.H266.Nals
{
    /// <summary>
    /// Represent a H265 stream writer settings class
    /// </summary>
    public sealed class H266StreamWriterSettings
    {
        /// <summary>
        /// Gets / Sets the DONL usage
        /// </summary>
        public bool DONL { get; set; }

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
        /// Gets / Sets the raw pps
        /// </summary>
        public byte[] RawPPS { get; set; }

        /// <summary>
        /// Gets / Sets the raw SPS
        /// </summary>
        public byte[] RawSPS { get; set; }

        /// <summary>
        /// Gets / Sets the raw vps
        /// </summary>
        public byte[] RawVPS { get; set; }

        




        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryValidate()
        {
            return VPS?.Length > 0 && RawVPS?.Length > 0
                && SPS?.Length > 0 && RawSPS?.Length > 0
                && PPS?.Length > 0 && RawPPS?.Length > 0
                ;
        }
        
        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            DONL = false;

            PPS = null;
            SPS = null;
            VPS = null;

            RawPPS = null;
            RawSPS = null;
            RawVPS = null;
        }
    }
}
