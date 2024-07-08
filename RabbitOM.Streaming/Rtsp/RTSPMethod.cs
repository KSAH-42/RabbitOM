using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the RTSP method type
    /// </summary>
    public enum RTSPMethod
    {
        /// <summary>
        /// Unknown
        /// </summary>
        UnDefined  = 0,

        /// <summary>
        /// The options method
        /// </summary>
        Options,

        /// <summary>
        /// The describe method
        /// </summary>
        Describe,

        /// <summary>
        /// The setup method
        /// </summary>
        Setup,

        /// <summary>
        /// The teardown method
        /// </summary>
        TearDown,

        /// <summary>
        /// The play method
        /// </summary>
        Play,

        /// <summary>
        /// The pause method
        /// </summary>
        Pause,

        /// <summary>
        /// The get parameter method
        /// </summary>
        GetParameter,

        /// <summary>
        /// The set parameter method
        /// </summary>
        SetParameter,

        /// <summary>
        /// The announce method
        /// </summary>
        Announce,

        /// <summary>
        /// The redirect method
        /// </summary>
        Redirect,

        /// <summary>
        /// The record method
        /// </summary>
        Record,
    }
}
