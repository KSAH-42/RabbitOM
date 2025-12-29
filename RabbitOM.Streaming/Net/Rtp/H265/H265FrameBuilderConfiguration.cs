using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent the H265 buidlder configuration class
    /// </summary>
    public sealed class H265FrameBuilderConfiguration : RtpFrameBuilderConfiguration
    {
        private byte[] _pps;

        private byte[] _sps;

        private byte[] _vps;





        /// <summary>
        /// Gets / Sets the PPS
        /// </summary>
        public byte[] PPS
        {
            get
            {
                lock ( SyncRoot )
                {
                    return _pps;
                }
            }

            set
            {
                lock ( SyncRoot )
                {
                    _pps = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the SPS
        /// </summary>
        public byte[] SPS
        {
            get
            {
                lock ( SyncRoot )
                {
                    return _sps;
                }
            }

            set
            {
                lock ( SyncRoot )
                {
                    _sps = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the VPS
        /// </summary>
        public byte[] VPS
        {
            get
            {
                lock ( SyncRoot )
                {
                    return _vps;
                }
            }

            set
            {
                lock ( SyncRoot )
                {
                    _vps = value;
                }
            }
        }
    }
}
