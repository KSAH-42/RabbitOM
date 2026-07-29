using System;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1998

namespace RabbitOM.Streaming.RtspV2.Transports
{
    // TODO: used correlator classes, readers and writers here

    public sealed class RtspRequester
    {
        public async Task SendMessageAsync( RtspInterleavedMessage interleavedData , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }

        // TODO: returns and let the higher level to raise an event handler for the receiving message otherwise lets any exception to bubble and decided that we have to do 

        public async Task<RtspResponseMessage> SendMessageAsync( RtspRequestMessage request , CancellationToken cancellationToken )
        {
            throw new NotImplementedException();
        }
    }
}

#pragma warning restore CS1998