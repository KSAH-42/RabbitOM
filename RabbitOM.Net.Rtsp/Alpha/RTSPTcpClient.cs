using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the client
    /// </summary>
    public sealed class RTSPTcpClient
        : RTSPClient
        , IRTSPClientConfigurable<RTSPTcpClientConfiguration>
    {

        /// <summary>
        /// Initialize an new instance of the client
        /// </summary>
        /// <param name="configuration">the configuration</param>
		public RTSPTcpClient(RTSPTcpClientConfiguration configuration )
		{
            throw new NotImplementedException();
        }







        /// <summary>
        /// Gets the sync root
        /// </summary>
        public override object SyncRoot
        {
            get => throw new NotImplementedException();
        }

		/// <summary>
		/// Check if the client is connected
		/// </summary>
		public override bool IsConnected
        {
            get => throw new NotImplementedException();
        }

        /// <summary>
        /// Check if the communication has been started
        /// </summary>
        public override bool IsCommunicationStarted
        {
            get => throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public RTSPTcpClientConfiguration Configuration 
        {
            get => throw new NotImplementedException();
        }








        /// <summary>
        /// Change the configuration
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        public void Configure(RTSPTcpClientConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Start the communication
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool StartCommunication()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stop the communication
        /// </summary>
        public override void StopCommunication()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stop the communication
        /// </summary>
        /// <param name="shutdownTimeout">the shutdown timeout</param>
        public override void StopCommunication( TimeSpan shutdownTimeout )
        {
            throw new NotImplementedException();
        }
    }
}
