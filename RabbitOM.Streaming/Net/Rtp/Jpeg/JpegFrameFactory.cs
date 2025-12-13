using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    /// <summary>
    /// Represent a frame factory class
    /// </summary>
    public sealed class JpegFrameFactory : IDisposable
    {
        private readonly JpegImageBuilder _imageBuilder = new JpegImageBuilder();

        /// <summary>
        /// Try to create a frame from aggregated packets
        /// </summary>
        /// <param name="packets">the packets</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _imageBuilder.Initialize();

            foreach ( var packet in packets )
            {
                if ( ! JpegFragment.TryParse( packet.Payload , out var fragment ) )
                {
                    return false;
                }

                if ( ! _imageBuilder.CanAddFragment( fragment ) )
                {
                    return false;
                }

                _imageBuilder.AddFragment( fragment );
            }

            if ( ! _imageBuilder.CanBuildImage() )
            {
                return false;
            }

            result = _imageBuilder.BuildFrame();

            return true;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _imageBuilder.Clear();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _imageBuilder.Dispose();
        }
    }
}
