using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameFactory : IDisposable
    {
        private readonly JpegImageBuilder _imageBuilder;

        public JpegFrameFactory()
        {
            _imageBuilder = new JpegImageBuilder();
        }

        public void Dispose()
        {
            _imageBuilder.Dispose();
        }

        public void Clear()
        {
            _imageBuilder.Clear();
        }

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
				Console.WriteLine( fragment );

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
