using System;

namespace RabbitOM.Decoder.FFMpeg.Domain.Media
{
    public interface IVideoDecoder : IDisposable
    {
        event EventHandler<FrameDecodedEventArgs>? FrameDecoded;

        bool IsOpened { get; }

        void Open( string type );

        void Close();

        bool CanConfigure( byte[] extraParameters );

        bool Configure( byte[] extraParameters );

        void Decode( byte[] buffer );
    }
}