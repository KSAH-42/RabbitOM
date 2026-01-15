using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    using RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging;
    using RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging.Fragmentation;

    /// <summary>
    /// Represent a frame factory class
    /// </summary>
    internal sealed class JpegFrameFactory : IDisposable
    {
        private readonly JpegImageBuilder _imageBuilder = new JpegImageBuilder();

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"/>
        public void Configure( JpegFrameBuilderConfiguration configuration )
        {
            if ( configuration == null )
            {
                throw new ArgumentNullException( nameof( configuration ) );
            }

            _imageBuilder.ResolutionFallback      = configuration.ResolutionFallBack;
            _imageBuilder.ForceResolutionFallBack = configuration.ForceResolutionFallBack;
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
