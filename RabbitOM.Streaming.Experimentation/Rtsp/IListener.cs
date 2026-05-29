using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    /// <summary>
    /// Represent the rtsp listener for receiving rtsp client request
    /// </summary>
    public interface IListener
    {
        event EventHandler Started;

        event EventHandler Stopped;


        bool IsStarted { get; }


        void Start();

        void Stop();
    }
}
