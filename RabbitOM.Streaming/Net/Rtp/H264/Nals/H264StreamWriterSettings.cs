using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Nals
{
    /// <summary>
    /// Represent a H264 stream settings
    /// </summary>
    public sealed class H264StreamWriterSettings
    {
        /// <summary>
        /// Gets / Sets the sps
        /// </summary>
        public byte[] SPS { get; set; }
        
        /// <summary>
        /// Gets / Sets the pps
        /// </summary>
        public byte[] PPS { get; set; }

        /// <summary>
        /// Gets / Sets the raw sps
        /// </summary>
        public byte[] RawSPS { get; set; }
        
        /// <summary>
        /// Gets / Sets the raw pps
        /// </summary>
        public byte[] RawPPS { get; set; }


        



        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns></returns>
        public bool TryValidate()
        {
            return SPS?.Length > 0 && RawSPS?.Length > 0 
                && PPS?.Length > 0 && RawPPS?.Length > 0
                ;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            SPS = null;
            PPS = null;

            RawSPS = null;
            RawPPS = null;
        }
    }
}
