using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent a H265 stream writer settings class
    /// </summary>
    public sealed class H265StreamWriterSettings
    {
        /// <summary>
        /// Gets / Sets the start code prefix
        /// </summary>
        public byte[] StartCodePrefix { get; set;  }

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
        /// Replace parameters
        /// </summary>
        /// <param name="settings">the settings</param>
        /// <param name="pps">the pps</param>
        /// <param name="sps">the sps</param>
        /// <param name="vps">the vps</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ReplaceSettings( H265StreamWriterSettings settings , byte[] startCodePrefix , byte[] pps , byte[] sps , byte[] vps )
        {
            if ( settings == null )
            {
                throw new ArgumentNullException( nameof( settings ) );
            }

            if ( settings.StartCodePrefix == null || settings.StartCodePrefix.Length == 0 )
            {
                settings.StartCodePrefix = startCodePrefix;
            }

            if ( settings.PPS == null || settings.PPS.Length == 0 )
            {
                settings.PPS = pps;
            }

            if ( settings.SPS == null || settings.SPS.Length == 0 )
            {
                settings.SPS = pps;
            }

            if ( settings.VPS == null || settings.VPS.Length == 0 )
            {
                settings.VPS = pps;
            }
        }







        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryValidate()
        {
            return StartCodePrefix?.Length > 0 && PPS?.Length > 0 && SPS?.Length > 0 && VPS?.Length > 0;
        }
        
        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            StartCodePrefix = null;
            PPS = null;
            SPS = null;
            VPS = null;
        }
    }
}
