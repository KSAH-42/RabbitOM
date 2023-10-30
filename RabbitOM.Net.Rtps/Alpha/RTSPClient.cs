using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the client
    /// </summary>
    public abstract class RTSPClient : IRTSPClient 
    {
        /// <summary>
        /// Raised when the communication has been started
        /// </summary>
        public event EventHandler<RTSPClientCommunicationStartedEventArgs> CommunicationStarted;

        /// <summary>
        /// Raised when the communication has been stopped
        /// </summary>
        public event EventHandler<RTSPClientCommunicationStoppedEventArgs> CommunicationStopped;

        /// <summary>
        /// Raised when the client is connected
        /// </summary>
        public event EventHandler<RTSPClientConnectedEventArgs> Connected;

        /// <summary>
        /// Raised when the client is disconnected
        /// </summary>
        public event EventHandler<RTSPClientDisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Raise when an data has been received
        /// </summary>
        public event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived;

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        public event EventHandler<RTSPClientErrorEventArgs> Error;







        /// <summary>
        /// Gets the sync root
        /// </summary>
        public abstract object SyncRoot
        {
            get;
        }

        /// <summary>
        /// Check if the client is connected
        /// </summary>
        public abstract bool IsConnected
        {
            get;
        }

        /// <summary>
        /// Check if the communication has been started
        /// </summary>
        public abstract bool IsCommunicationStarted
        {
            get;
        }






        /// <summary>
        /// Start the communication
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool StartCommunication();

        /// <summary>
        /// Stop the communication
        /// </summary>
        public abstract void StopCommunication();

        /// <summary>
        /// Stop the communication
        /// </summary>
        /// <param name="shutdownTimeout">the shutdown timeout</param>
        public abstract void StopCommunication( TimeSpan shutdownTimeout );







        /// <summary>
        /// Occurs when the communication has been started
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        protected virtual void OnCommunicationStarted( object sender , RTSPClientCommunicationStartedEventArgs e )
        {
            CommunicationStarted?.TryInvoke( sender , e );
        }

        /// <summary>
        /// Occurs when the communication has been stopped
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        protected virtual void OnCommunicationStopped( object sender , RTSPClientCommunicationStoppedEventArgs e )
        {
            CommunicationStopped?.TryInvoke( sender , e );
        }

        /// <summary>
        /// Occurs when the client is connected
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        protected virtual void OnConnected( object sender , RTSPClientConnectedEventArgs e )
        {
            Connected?.TryInvoke( sender ,e );
        }

        /// <summary>
        /// Occurs when the client is disconnected
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        protected virtual void OnDisconnected( object sender , RTSPClientDisconnectedEventArgs e )
        {
            Disconnected?.TryInvoke( sender , e );
        }

        /// <summary>
        /// Occurs when an data has been received
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        protected virtual void OnPacketReceived( object sender , RTSPPacketReceivedEventArgs e )
        {
            PacketReceived?.TryInvoke( sender , e );
        }

        /// <summary>
        /// Occurs when an error occurs
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        protected virtual void OnError( object sender , RTSPClientErrorEventArgs e )
        {
            Error?.TryInvoke( sender , e );
        }
    }
}
