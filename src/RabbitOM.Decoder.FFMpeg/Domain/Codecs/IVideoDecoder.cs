using System;

namespace RabbitOM.Decoder.FFMpeg.Domain.Codecs
{
    public interface IVideoDecoder : IDisposable
    {
        event EventHandler<FrameDecodedEventArgs>? FrameDecoded;

        public bool IsOpened { get; }

        public void Open();

        public void Close();

        public bool CanConfigure( byte[] extraParameters );

        public bool Configure( byte[] extraParameters );

        public void Decode( byte[] buffer );
    }
}