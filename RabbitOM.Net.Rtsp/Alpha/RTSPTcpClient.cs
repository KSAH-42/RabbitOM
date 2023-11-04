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
        public RTSPTcpClient()
		{
            throw new NotImplementedException();
        }

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
        /// Change the configuration only when the current instance is not running
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        public void Configure(RTSPTcpClientConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change the configuration only when the current instance is not running
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryConfigure(RTSPTcpClientConfiguration configuration)
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

        /// <summary>
        /// This method will block the calling thread until the client has establish the connection
        /// </summary>
        /// <param name="timeout">the time to wait</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///     <para>This method will returns false in case the communication is not established.</para>
        ///     <para>And it will returns true when the communication is connected or already connected.</para>
        /// </remarks>
        public override bool WaitForConnection(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }








        /// <summary>
        /// Gets the idle timeout
        /// </summary>
        /// <returns>returns a value</returns>
        protected override TimeSpan GetIdleTimeout()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the retry timeout
        /// </summary>
        /// <returns>returns a value</returns>
        protected override TimeSpan GetRetryTimeout()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Wait the cancelation
        /// </summary>
        /// <param name="span">the time span</param>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected override bool DoWaitCancelation(TimeSpan span)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process a shutdown
        /// </summary>
        protected override bool DoShutdown()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process a connect operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected override bool DoConnect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process a disconnect operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected override bool DoDisconnect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process a get options operaiton
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected override bool DoGetOptions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process a setup operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected override bool DoSetup()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process a play operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected override bool DoPlay()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process a teardown operation
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected override bool DoTeardown()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process a ping operation used to maintain the network communication with the remote rtsp source
        /// </summary>
        /// <returns>returns true for a sucess, otherwise false</returns>
        protected override bool DoHeartBeat()
        {
            throw new NotImplementedException();
        }
    }
}
