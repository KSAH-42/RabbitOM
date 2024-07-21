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

        // a try catch block must be add added
        // and not tryMethod on the stream class because there is a loop
        // it is better to leave the loop when a failure happens inside it.

        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _stream.Clear();

            _stream.WriteStartOfImage();
            _stream.WriteApplicationJFIF();

            foreach ( var packet in packets )
            {
                if ( ! JpegFragment.TryParse( packet.Payload , out JpegFragment fragment ) )
                {
                    return false;
                }

                // select the right stream.WriteXXXX method 

                throw new NotImplementedException();
            }

            _stream.WriteEndOfImage();

            result = new RtpFrame( _stream.ToArray() );

            return true;
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
