using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an udp socket
    /// </summary>
    internal sealed class RTSPSocketMulticastReceiver : IDisposable
    {
        /// <summary>
        /// Represent the socket buffer size
        /// </summary>
        public const int DefaultReceiveBufferSize = 8096*3;




        private IPAddress   _ipAddress       = null;

        private IPEndPoint  _groupEP         = null;

        private UdpClient   _udpClient       = null;





        /// <summary>
        /// Check if the socket has been opened
        /// </summary>
        public bool IsOpened
        {
            get => _udpClient != null;
        }





        /// <summary>
        /// Open the socket
        /// </summary>
        /// <param name="ipAddress">the ip address</param>
        /// <param name="port">the port</param>
        /// <param name="ttl">the ttl</param>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Open( string ipAddress , int port , int ttl , TimeSpan receiveTimeout )
        {
            if ( string.IsNullOrWhiteSpace( ipAddress ) || port < 0 || ttl < 0 )
            {
                return false;
            }

            if ( _udpClient != null )
            {
                return false;
            }

            if ( !IPAddress.TryParse( ipAddress , out IPAddress address ) || address == null )
            {
                return false;
            }

            try
            {
                var groupEP = new IPEndPoint( IPAddress.Any , port );

                var udpClient = new UdpClient();

                udpClient.ExclusiveAddressUse = false;
                udpClient.Client.SetSocketOption( SocketOptionLevel.Socket , SocketOptionName.ReuseAddress , true );
                udpClient.Client.ReceiveTimeout = (int) receiveTimeout.TotalMilliseconds;
                udpClient.Client.ReceiveBufferSize = DefaultReceiveBufferSize;
                udpClient.JoinMulticastGroup( address , ttl );
                udpClient.Client.Bind( groupEP );

                _udpClient = udpClient;
                _groupEP = groupEP;
                _ipAddress = address;

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
                if ( _udpClient != null && _ipAddress != null )
                {
                    _udpClient.DropMulticastGroup( _ipAddress );
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            try
            {
                if ( _udpClient != null )
                {
                    _udpClient.Close();
                    _udpClient.Dispose();
                }
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            _udpClient = null;
            _groupEP = null;
            _ipAddress = null;
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Wait data during the specified timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>return true for a success, otherwise false</returns>
        public bool PollReceive( TimeSpan timeout )
        {
            if ( _udpClient == null )
            {
                return false;
            }

            try
            {
                return _udpClient.Client.Poll( (int) ( timeout.TotalMilliseconds * 1000 ) , SelectMode.SelectRead );
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
        public byte[] Receive()
        {
            if ( _udpClient == null || _groupEP == null )
            {
                return null;
            }

            try
            {
                return _udpClient.Receive( ref _groupEP );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return null;
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
