using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming.Media
{
    public interface IVideoOutput : IDisposable
    {
        bool IsOpened { get; }

        void Open();

        void Close();

        void SendImage( Image image );
    }
}