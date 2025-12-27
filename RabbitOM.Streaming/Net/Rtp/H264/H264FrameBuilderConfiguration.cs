/*
 THIS IMPLEMENTATION IS NOT FINISH AND NOT TESTED DO NOT USED IT IN PRODUCTION
 */

using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public sealed class H264FrameBuilderConfiguration : RtpFrameBuilderConfiguration
    {
        private byte[] _startCodePrefix = H264Constants.StartCodePrefixV1;

        private byte[] _pps;

        private byte[] _sps;



        public byte[] StartCodePrefix
        {
            get
            {
                lock ( SyncRoot )
                {
                    return _startCodePrefix;
                }
            }

            set
            {
                lock ( SyncRoot )
                {
                    _startCodePrefix = value;
                }
            }
        }

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
    }
}
