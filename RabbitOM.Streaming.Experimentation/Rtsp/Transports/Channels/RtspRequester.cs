using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    // TODO: used correlator classes, readers and writers

    public sealed class RtspRequester
    {
        public async Task SendMessageAsync( RtspInterleavedMessage interleavedData , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        // TODO: returns and let the higher level to raise an event handler for the message received

        public async Task<RtspResponseMessage> SendMessageAsync( RtspRequestMessage request , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }
    }
}
