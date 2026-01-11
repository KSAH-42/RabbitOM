using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H266
{
    using RabbitOM.Streaming.Net.Rtp.H266.Nals;

    internal sealed class H266FrameFactory : IDisposable
    {
        private readonly H266StreamWriter _writer = new H266StreamWriter();

        public void Configure( H266FrameBuilderConfiguration configuration )
        {
            throw new NotImplementedException();
        }

        public bool TryCreate( IEnumerable<RtpPacket> packets , out H266FrameMediaElement result )
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
