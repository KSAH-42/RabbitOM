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

        Task OpenAsync( CancellationToken cancellationToken = default );

        Task CloseAsync( CancellationToken cancellationToken = default );

        Task AbortAsync( CancellationToken cancellationToken = default );

        Task SendMessageAsync( RtspInterleavedMessage interleavedData , CancellationToken cancellationToken = default );

        Task<RtspResponseMessage> SendMessageAsync( RtspRequestMessage request , CancellationToken cancellationToken = default );
    }
}
