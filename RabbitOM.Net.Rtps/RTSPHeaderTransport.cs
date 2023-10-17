using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderTransport : RTSPHeader
    {
        private RTSPTransportType     _type             = RTSPTransportType.Unknown;

        private RTSPTransmissionType  _transmissionType = RTSPTransmissionType.Unknown;

        private byte                  _ttl              = 0;

        private int                   _layers           = 0;

        private string                _source           = string.Empty;

        private string                _destination      = string.Empty;

        private string                _ssrc             = string.Empty;

        private string                _mode             = string.Empty;

        private RTSPPortPair          _clientPort       = RTSPPortPair.Zero;

        private RTSPPortPair          _serverPort       = RTSPPortPair.Zero;

        private RTSPPortPair          _interleavedPort  = RTSPPortPair.Zero;

        private RTSPPortPair          _port             = RTSPPortPair.Zero;






        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <returns>returns an instance</returns>
        public static RTSPHeaderTransport NewInterleavedTransportHeader()
        {
            return NewInterleavedTransportHeader( 0 );
        }

        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <param name="port">the port number</param>
        /// <returns>returns an instance</returns>
        public static RTSPHeaderTransport NewInterleavedTransportHeader( int port )
        {
            return new RTSPHeaderTransport()
            {
                TransmissionType = RTSPTransmissionType.Unicast ,
                Type = RTSPTransportType.RTP_AVP_TCP ,
                InterleavedPort = RTSPPortPair.NewPortPair( port )
            };
        }

        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <param name="port">the port number</param>
        /// <returns>returns an instance</returns>
        public static RTSPHeaderTransport NewUnicastUdpTransportHeader( int port )
        {
            return new RTSPHeaderTransport()
            {
                TransmissionType = RTSPTransmissionType.Unicast ,
                Type = RTSPTransportType.RTP_AVP_UDP ,
                ClientPort = RTSPPortPair.NewPortPair( port )
            };
        }

        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <param name="port">the port number</param>
        /// <param name="ipAddress">the multicast destination address</param>
        /// <returns>returns an instance</returns>
        public static RTSPHeaderTransport NewMulticastUdpTransportHeader( string ipAddress , int port )
        {
            return NewMulticastUdpTransportHeader( ipAddress , port , 5 );
        }

        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <param name="port">the port number</param>
        /// <param name="ipAddress">the multicast destination address</param>
        /// <param name="ttl">the ttl</param>
        /// <returns>returns an instance</returns>
        public static RTSPHeaderTransport NewMulticastUdpTransportHeader( string ipAddress , int port , byte ttl )
        {
            return NewMulticastUdpTransportHeader( ipAddress , port , ttl , string.Empty );
        }

        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <param name="port">the port number</param>
        /// <param name="ipAddress">the multicast destination address</param>
        /// <param name="ttl">the ttl</param>
        /// <param name="mode">the mode</param>
        /// <returns>returns an instance</returns>
        public static RTSPHeaderTransport NewMulticastUdpTransportHeader( string ipAddress , int port , byte ttl , string mode )
        {
            return new RTSPHeaderTransport()
            {
                TransmissionType = RTSPTransmissionType.Multicast ,
                Type = RTSPTransportType.RTP_AVP_UDP ,
                ClientPort = RTSPPortPair.NewPortPair( port ) ,
                Destination = ipAddress ,
                TTL = ttl ,
                Mode = mode ,
            };
        }






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.Transport;
        }

        /// <summary>
        /// Gets / Sets the transport
        /// </summary>
        public RTSPTransportType Type
        {
            get => _type;
            set => _type = value;
        }

        /// <summary>
        /// Gets / Sets the transmission type
        /// </summary>
        public RTSPTransmissionType TransmissionType
        {
            get => _transmissionType;
            set => _transmissionType = value;
        }

        /// <summary>
        /// Gets / Sets the ttl
        /// </summary>
        public byte TTL
        {
            get => _ttl;
            set => _ttl = value;
        }

        /// <summary>
        /// Gets / Sets the layers
        /// </summary>
        public int Layers
        {
            get => _layers;
            set => _layers = value;
        }

        /// <summary>
        /// Gets / Sets the source
        /// </summary>
        public string Source
        {
            get => _source;
            set => _source = RTSPDataFilter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the destination
        /// </summary>
        public string Destination
        {
            get => _destination;
            set => _destination = RTSPDataFilter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the ssrc
        /// </summary>
        public string SSRC
        {
            get => _ssrc;
            set => _ssrc = RTSPDataFilter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the mode
        /// </summary>
        public string Mode
        {
            get => _mode;
            set => _mode = RTSPDataFilter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the client ports
        /// </summary>
        public RTSPPortPair ClientPort
        {
            get => _clientPort;
            set => _clientPort = value;
        }

        /// <summary>
        /// Gets / Sets the server ports
        /// </summary>
        public RTSPPortPair ServerPort
        {
            get => _serverPort;
            set => _serverPort = value;
        }

        /// <summary>
        /// Gets / Sets the interleaved ports
        /// </summary>
        public RTSPPortPair InterleavedPort
        {
            get => _interleavedPort;
            set => _interleavedPort = value;
        }

        /// <summary>
        /// Gets / Sets the port
        /// </summary>
        public RTSPPortPair Port
        {
            get => _port;
            set => _port = value;
        }






        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( _type == RTSPTransportType.Unknown )
            {
                return false;
            }

            if ( _transmissionType == RTSPTransmissionType.Unknown )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            if ( _type == RTSPTransportType.Unknown || _transmissionType == RTSPTransmissionType.Unknown )
            {
                return string.Empty;
            }

            var writer = new RTSPHeaderWriter( RTSPSeparator.SemiColon , RTSPOperator.Equality );

            writer.Write( RTSPTransportTypeConverter.Convert( _type ) );
            writer.WriteSeparator();

            writer.Write( RTSPTransmissionTypeConverter.Convert( _transmissionType ) );

            if ( !string.IsNullOrWhiteSpace( _destination ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.Destination , _destination );
            }

            if ( RTSPPortPair.IsNotNull( _interleavedPort ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.Interleaved , _interleavedPort.ToString() );
            }

            if ( 0 < _ttl )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.TTL , _ttl );
            }

            if ( 0 < _layers )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.Layers , _layers );
            }

            if ( RTSPPortPair.IsNotNull( _port ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.Port , _port.ToString() );
            }

            if ( RTSPPortPair.IsNotNull( _clientPort ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.ClientPort , _clientPort.ToString() );
            }

            if ( RTSPPortPair.IsNotNull( _serverPort ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.ServerPort , _serverPort.ToString() );
            }

            if ( !string.IsNullOrWhiteSpace( _ssrc ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.SSRC , _ssrc );
            }

            if ( !string.IsNullOrWhiteSpace( _mode ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.Mode , _mode );
            }

            return writer.Output;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RTSPHeaderTransport result )
        {
            result = null;

            var parser = new RTSPParser( value , RTSPSeparator.SemiColon );

            if ( !parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RTSPHeaderReader( parser.GetParsedHeaders() ) )
            {
                reader.Operator = RTSPOperator.Equality;
                reader.IncludeQuotes = true;

                result = new RTSPHeaderTransport();

                while ( reader.Read() )
                {
                    if ( reader.SplitElementAsField() )
                    {
                        #region PARSE FIELDS

                        if ( reader.IsDestinationElementType )
                        {
                            result.Destination = reader.GetElementValue();
                        }

                        if ( reader.IsInterleavedElementType )
                        {
                            result.InterleavedPort = RTSPPortPair.TryParse( reader.GetElementValue() , out RTSPPortPair port ) ? port : RTSPPortPair.Zero;
                        }

                        if ( reader.IsTtlElementType )
                        {
                            result.TTL = reader.GetElementValueAsByte();
                        }

                        if ( reader.IsLayersElementType )
                        {
                            result.Layers = reader.GetElementValueAsInteger();
                        }

                        if ( reader.IsPortElementType )
                        {
                            result.Port = RTSPPortPair.TryParse( reader.GetElementValue() , out RTSPPortPair port ) ? port : RTSPPortPair.Zero;
                        }

                        if ( reader.IsClientPortElementType )
                        {
                            result.ClientPort = RTSPPortPair.TryParse( reader.GetElementValue() , out RTSPPortPair port ) ? port : RTSPPortPair.Zero;
                        }

                        if ( reader.IsServerPortElementType )
                        {
                            result.ServerPort = RTSPPortPair.TryParse( reader.GetElementValue() , out RTSPPortPair port ) ? port : RTSPPortPair.Zero;
                        }

                        if ( reader.IsSSRCElementType )
                        {
                            result.SSRC = reader.GetElementValue();
                        }

                        if ( reader.IsModeElementType )
                        {
                            result.Mode = reader.GetElementValue();
                        }

                        #endregion
                    }
                    else
                    {
                        #region PARSE SIMPLE ELEMENTS

                        if ( reader.IsRtpAvpElementType || reader.IsRtpAvpUdpElementType )
                        {
                            result.Type = RTSPTransportType.RTP_AVP_UDP;
                        }

                        if ( reader.IsRtpAvpTcpElementType )
                        {
                            result.Type = RTSPTransportType.RTP_AVP_TCP;
                        }

                        if ( reader.IsMulticastElementType )
                        {
                            result.TransmissionType = RTSPTransmissionType.Multicast;
                        }

                        if ( reader.IsUnicastElementType )
                        {
                            result.TransmissionType = RTSPTransmissionType.Unicast;
                        }

                        #endregion
                    }
                }

                return true;
            }
        }
    }
}
