using System;

namespace RabbitOM.Player
{
    public interface IApplication
    {
        string StreamUri { get; set; }

        void ShowStatistics();

        void HideStatistics();

        void StrechImage();

        void UnStrechImage();

        void StartStreaming();

        void StopStreaming();
<<<<<<< HEAD
=======

        void ShowHelp();
>>>>>>> fd902550 (add application interface)
    }
}
