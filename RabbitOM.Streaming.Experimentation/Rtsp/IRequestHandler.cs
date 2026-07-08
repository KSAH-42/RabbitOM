using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public interface IRequestHandler
    {
        Task<RtspClientResponse> SendRequestAsync( RtspClientRequest request , CancellationToken token );
    }
}
