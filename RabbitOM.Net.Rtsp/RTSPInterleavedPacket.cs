﻿using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an interleaved packet
    /// </summary>
    public sealed class RTSPInterleavedPacket : RTSPPacket
    {
        private readonly int _channel = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel">the channek</param>
        /// <param name="data">the data</param>
        public RTSPInterleavedPacket( int channel , byte[] data )
            : base( data )
        {
            _channel = channel;
        }

        /// <summary>
        /// Gets the channel number
        /// </summary>
        public int Channel
        {
            get => _channel;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( _channel < 0 )
            {
                return false;
            }

            return base.TryValidate();
        }
    }
}
