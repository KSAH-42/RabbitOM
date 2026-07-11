using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public interface IChannel : IDisposable
    {
        event EventHandler Opened;

        event EventHandler Closed;

        event EventHandler Aborted;

        event EventHandler<RtspMessageEventArgs> MessageReceived;




        bool IsOpened { get; }





        void Open();

        void Close();

        void Abort();

        Task OpenAsync();

        Task CloseAsync();

        Task AbortAsync();

        Task SendMessageAsync( RtspInterleavedMessage interleavedData , CancellationToken cancellationToken );

        Task<RtspResponseMessage> SendMessageAsync( RtspRequestMessage request , CancellationToken cancellationToken );
    }
}
