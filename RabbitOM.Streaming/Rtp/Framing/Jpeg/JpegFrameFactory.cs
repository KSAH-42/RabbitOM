using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent a frame factory class
    /// </summary>
    public sealed class JpegFrameFactory : IDisposable
    {
        private readonly JpegImageBuilder _imageBuilder;

        /// <summary>
        /// Initialize an instance
        /// </summary>
        public JpegFrameFactory()
        {
            _imageBuilder = new JpegImageBuilder();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _imageBuilder.Dispose();
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _imageBuilder.Clear();
        }

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

            _imageBuilder.Clear();

            foreach ( RtpPacket packet in packets )
            {
                if ( ! JpegFragment.TryParse( packet.Payload , out JpegFragment fragment ) )
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

            result = JpegFrame.NewFrame( _imageBuilder.BuildImage() );

            return true;
        }
    }
}
