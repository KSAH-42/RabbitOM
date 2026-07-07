using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public interface IClientChannel : IDisposable
    {
        event EventHandler Opened;

        event EventHandler Closed;

        event EventHandler Aborted;

        event EventHandler<RtspMessageEventArgs> MessageReceived;

        bool IsOpened { get; }

        Task OpenAsync( CancellationTokenSource cancellationToken = default );

        Task CloseAsync( CancellationTokenSource cancellationToken = default );

        Task AbortAsync( CancellationTokenSource cancellationToken = default );

        Task SendMessageAsync( RtspInterleavedMessage interleavedData , CancellationTokenSource cancellationToken = default );

        Task<RtspResponseMessage> SendMessageAsync( RtspRequestMessage request , CancellationTokenSource cancellationToken = default );
    }
}
