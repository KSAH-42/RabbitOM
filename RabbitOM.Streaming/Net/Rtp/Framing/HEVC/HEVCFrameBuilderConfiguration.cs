using System;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCFrameBuilderConfiguration : RtpFrameBuilderConfiguration
    {
        private byte[] _pps;

        private byte[] _sps;

        private byte[] _vps;



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
