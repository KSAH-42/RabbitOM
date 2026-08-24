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

        void ShowHelp();
=======
<<<<<<< HEAD
=======

        void ShowHelp();
>>>>>>> fd902550 (add application interface)
>>>>>>> 300ee1113ff19ac88f082c7638d1c5f4f8745e1c
    }
}
