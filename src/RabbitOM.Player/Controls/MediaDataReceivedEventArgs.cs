using System;

namespace RabbitOM.Player.Controls
{
    public sealed class MediaDataReceivedEventArgs : EventArgs
    {
        public MediaDataReceivedEventArgs( byte[] data )
        {
            Data = data;
        }

        public byte[] Data { get; }
    }
}
