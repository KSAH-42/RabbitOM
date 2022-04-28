using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an udp socket that only received 
    /// </summary>
    public sealed class RTSPSocketUdpReceiverAsync
    {
        /// <summary>
        /// Represent the socket buffer size
        /// </summary>
        public const int DefaultReceiveBufferSize = 500000;


        private EndPoint  _groupEP  = null;

        private UdpClient   _socket   = null;

        private IAsyncResult _asyncResult = null;

        private readonly Action<byte[]> _dataReceivedCallback = null;

        private readonly byte[] _buffer = null;


        /// TODO: Add a lock


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataReceivedCallback">the callback invoked when data has been received</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPSocketUdpReceiverAsync( Action<byte[]> dataReceivedCallback)
		{
            _dataReceivedCallback = dataReceivedCallback ?? throw new ArgumentNullException( nameof(dataReceivedCallback ) );
            _buffer = new byte[DefaultReceiveBufferSize];
        }





        /// <summary>
        /// Check if the socket has been opened
        /// </summary>
        public bool IsOpened
        {
            get => _socket != null;
        }

        /// <summary>
        /// Check if the start receive has been initiated
        /// </summary>
        public bool IsReceiving
		{
            get => _asyncResult != null && ! _asyncResult.IsCompleted;

        }





        /// <summary>
        /// Open the socket
        /// </summary>
        /// <param name="port">the port</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Open( int port )
        {
            if ( port < 0 )
            {
                return false;
            }

            if ( _socket != null )
            {
                return false;
            }

            try
            {
                var groupEP = new IPEndPoint( IPAddress.Any, port);

                var socket = new UdpClient( port );

                socket.Client.ReceiveBufferSize = _buffer.Length;

                _socket = socket;
                _groupEP = groupEP;

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Close the socket
        /// </summary>
        public void Close()
        {
            try
            {
                if ( _socket != null )
                {
                    _socket.Close();
                    _socket.Dispose();
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            _socket = null;
            _groupEP = null;
            _asyncResult = null;
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Gets the receive timeout
        /// </summary>
        /// <returns>returns a value</returns>
        public TimeSpan GetReceiveTimeout()
        {
            if ( _socket == null )
            {
                return TimeSpan.Zero;
            }

            try
            {
                return TimeSpan.FromMilliseconds( _socket.Client.ReceiveTimeout );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Set the receive timeout
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SetReceiveTimeout( TimeSpan value )
        {
            if ( _socket == null )
            {
                return false;
            }

            try
            {
                _socket.Client.ReceiveTimeout = (int) value.TotalMilliseconds;

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <returns>returns an array of bytes, otherwise null</returns>
        public bool StartReceive()
        {
            if ( _socket == null || _groupEP == null )
            {
                return false;
            }

            if ( _asyncResult != null && ! _asyncResult.IsCompleted )
			{
                return false;
			}

            try
            {
                
                _asyncResult = _socket.Client.BeginReceiveFrom(_buffer , 0 , _buffer.Length , SocketFlags.None , ref _groupEP , ProcessReceive, null);

                return _asyncResult != null;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            _asyncResult = null;

            return false;
        }

        /// <summary>
        /// Stop receiving packet
        /// </summary>
        public void StopReceive()
		{
            if ( _socket == null || _groupEP == null )
			{
                return;
			}

            if ( _asyncResult == null )
			{
                return;
			}

			try
			{
                _socket.Client.EndReceiveFrom(_asyncResult, ref _groupEP );
			}
			catch (Exception ex)
			{
                OnError(ex);
			}

            _asyncResult = null;
        }

        /// <summary>
        /// Handle data received
        /// </summary>
        /// <param name="ar">the async result</param>
        private void ProcessReceive(IAsyncResult ar)
        {
            if (ar == null || _socket == null || _groupEP == null)
            {
                return;
            }

            try
            {
                int bytesReceived = _socket.Client.EndReceiveFrom( ar , ref _groupEP);

                if (bytesReceived > 0 )
				{
                    var data = new byte[bytesReceived];

                    System.Buffer.BlockCopy(_buffer, 0, data, 0, bytesReceived);

                    OnDataReceived(data);
				}
            }
            catch (Exception ex)
            {
                OnError(ex);
            }

            _asyncResult = null;
            StartReceive();
        }







        /// <summary>
        /// Occurs when packet has been received
        /// </summary>
        /// <param name="data">the data</param>
        private void OnDataReceived( byte[] data )
		{
            try
			{
                _dataReceivedCallback.Invoke(data);
            }
			catch (Exception ex)
			{
                OnError(ex);
			}
		}

        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="exception">the exception</param>
        private void OnError( Exception exception )
        {
            if ( exception == null )
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine( exception );
        }
    }
}
