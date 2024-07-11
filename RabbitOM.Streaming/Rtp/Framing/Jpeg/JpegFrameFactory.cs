using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameFactory : IDisposable
    {
        private readonly JpegFrameBuilder _builder;
        private readonly JpegStreamWriter _stream;

        public JpegFrameFactory( JpegFrameBuilder builder )
        {
            _builder = builder ?? throw new ArgumentNullException( nameof( builder ) );
            _stream = new JpegStreamWriter();
        }


        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _stream.Clear();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
