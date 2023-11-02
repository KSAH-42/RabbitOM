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

        /// <summary>
        /// Change the configuration only when the current instance is not running
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        void Configure( TConfiguration configuration );

        /// <summary>
        /// Change the configuration only when the current instance is not running
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool TryConfigure(TConfiguration configuration);
    }
}
