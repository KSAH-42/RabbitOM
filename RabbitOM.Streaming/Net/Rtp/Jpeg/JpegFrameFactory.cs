using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    using RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging;
    using RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging.Fragmentation;

    /// <summary>
    /// Represent a frame factory class
    /// </summary>
    public sealed class JpegFrameFactory : IDisposable
    {
        private readonly JpegImageBuilder _imageBuilder = new JpegImageBuilder();

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="resolutionFallBack">the resolution fallback</param>
        /// <exception cref="ArgumentNullException"/>
        public void ConfigureResolutionFallBack( JpegResolution resolutionFallBack )
        {
            if ( JpegResolution.IsNullOrEmpty( resolutionFallBack ) )
            {
                throw new ArgumentNullException( nameof( resolutionFallBack ) );
            }

            _imageBuilder.ResolutionFallBack = resolutionFallBack;
        }

        /// <summary>
        /// Try to create a frame from aggregated packets
        /// </summary>
        /// <param name="packets">the packets</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out JpegFrameMediaElement result )
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

            if ( _imageBuilder.CanBuildFrame() )
            {
                result = _imageBuilder.BuildFrame();
            }

            return result != null;
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
