using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    /// <summary>
    /// Represente the H264 frame builder configuration
    /// </summary>
    public sealed class H264FrameBuilderConfiguration : RtpFrameBuilderConfiguration
    {
        private byte[] _pps;

        private byte[] _sps;





        /// <summary>
        /// Gets / Sets the pps
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
        /// Gets / Sets the sps
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
    }
}
