using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCFrameFactory : IDisposable
    {
        private readonly HEVCFrameBuilderConfiguration _configuration;
        private readonly HEVCStreamWriter _writer;

        public HEVCFrameFactory( HEVCFrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
            _writer = new HEVCStreamWriter();
        } 

        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.SetLength( 0 );

            _writer.PPS = _writer.PPS?.Length > 0 ? _writer.PPS : _configuration.PPS;
            _writer.SPS = _writer.SPS?.Length > 0 ? _writer.SPS : _configuration.SPS;
            _writer.VPS = _writer.VPS?.Length > 0 ? _writer.VPS : _configuration.VPS;
     
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _writer.Clear();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
