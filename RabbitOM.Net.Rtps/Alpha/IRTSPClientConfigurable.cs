using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the client configuration entry point
    /// </summary>
    /// <typeparam name="TConfiguration">type of the configuration</typeparam>
    public interface IRTSPClientConfigurable<TConfiguration>
        where TConfiguration : RTSPClientConfiguration
    {
        /// <summary>
        /// Gets the configuration
        /// </summary>
        TConfiguration Configuration
        {
            get;
        }
    }
}
