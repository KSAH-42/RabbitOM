using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    // TODO: used correlator classes, readers and writers

    public sealed class RtspRequester
    {
        public Task SendMessageAsync( RtspInterleavedMessage interleavedData , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        // TODO: returns and let the higher level to raise an event handler for the receiving message otherwise lets bubble the failure 

        public Task<RtspResponseMessage> SendMessageAsync( RtspRequestMessage request , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }
    }
}
