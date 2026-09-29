using System;
using System.Windows;

namespace RabbitOM.Player
{
    public interface IApplication
    {
        Window Window { get; }

        void StartStreaming();
        void StartStreaming( string uri );
        void StopStreaming();
    }
}
