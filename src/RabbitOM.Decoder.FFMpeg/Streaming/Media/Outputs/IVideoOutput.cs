using RabbitOM.Decoder.FFMpeg.Streaming.Media;
using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming.Media.Outputs
{
    public interface IVideoOutput : IDisposable
    {
        bool IsOpened { get; }

        void Open();

        void Close();

        void SendImage( Image image );
    }
}