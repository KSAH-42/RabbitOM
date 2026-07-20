using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Jpeg
{
    using RabbitOM.Streaming.Rtp.Jpeg.Imaging;

    public sealed class JpegFrameFactory : IDisposable
    {
        private readonly JpegImageBuilder _imageBuilder = new JpegImageBuilder();



        public ResolutionInfo? ResolutionFallback
        {
            get => _imageBuilder.ResolutionFallback;
            set => _imageBuilder.ResolutionFallback = value;
        }



        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out JpegMediaElement result )
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

                if ( ! _imageBuilder.AddFragment( fragment ) )
                {
                    return false;
                }
            }

            if ( _imageBuilder.CanBuildFrame() )
            {
                result = _imageBuilder.BuildFrame();
            }

            return result != null;
        }

        public void Clear()
        {
            _imageBuilder.Clear();
        }

        public void Dispose()
        {
            _imageBuilder.Dispose();
        }
    }
}
