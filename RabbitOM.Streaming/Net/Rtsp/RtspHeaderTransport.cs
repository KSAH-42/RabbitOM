using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderTransport : RtspHeader
    {
        private RtspTransportType     _transportType    = RtspTransportType.Unknown;

        private RtspTransmissionType  _transmissionType = RtspTransmissionType.Unknown;

        private byte                  _ttl              = 0;

        private int                   _layers           = 0;

        private string                _source           = string.Empty;

        private string                _destination      = string.Empty;

        private string                _ssrc             = string.Empty;

        private string                _mode             = string.Empty;

        private RtspPortPair          _clientPort       = RtspPortPair.Zero;

        private RtspPortPair          _serverPort       = RtspPortPair.Zero;

        private RtspPortPair          _interleavedPort  = RtspPortPair.Zero;

        private RtspPortPair          _port             = RtspPortPair.Zero;






        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <returns>returns an instance</returns>
        public static RtspHeaderTransport NewInterleavedTransportHeader()
        {
            return NewInterleavedTransportHeader( 0 );
        }

        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <param name="port">the port number</param>
        /// <returns>returns an instance</returns>
        public static RtspHeaderTransport NewInterleavedTransportHeader( int port )
        {
            return new RtspHeaderTransport()
            {
                TransmissionType = RtspTransmissionType.Unicast ,
                TransportType    = RtspTransportType.RTP_AVP_TCP ,
                InterleavedPort  = RtspPortPair.NewPortPair( port )
            };
        }

        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <param name="port">the port number</param>
        /// <returns>returns an instance</returns>
        public static RtspHeaderTransport NewUnicastUdpTransportHeader( int port )
        {
            return new RtspHeaderTransport()
            {
                TransmissionType = RtspTransmissionType.Unicast ,
                TransportType    = RtspTransportType.RTP_AVP_UDP ,
                ClientPort       = RtspPortPair.NewPortPair( port )
            };
        }

        /// <summary>
        /// Create a new transport header for unicast streaming
        /// </summary>
        /// <param name="port">the port number</param>
        /// <param name="ipAddress">the multicast destination address</param>
        /// <returns>returns an instance</returns>
        public static RtspHeaderTransport NewMulticastUdpTransportHeader( string ipAddress , int port )
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
        public static RtspHeaderTransport NewMulticastUdpTransportHeader( string ipAddress , int port , byte ttl )
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
        public static RtspHeaderTransport NewMulticastUdpTransportHeader( string ipAddress , int port , byte ttl , string mode )
        {
            return new RtspHeaderTransport()
            {
                TransmissionType = RtspTransmissionType.Multicast ,
                TransportType    = RtspTransportType.RTP_AVP_UDP ,
                ClientPort       = RtspPortPair.NewPortPair( port ) ,
                Destination      = ipAddress ,
                TTL              = ttl ,
                Mode             = mode ,
            };
        }






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Transport;
        }

        /// <summary>
        /// Gets / Sets the transport
        /// </summary>
        public RtspTransportType TransportType
        {
            get => _transportType;
            set => _transportType = value;
        }

        /// <summary>
        /// Gets / Sets the transmission type
        /// </summary>
        public RtspTransmissionType TransmissionType
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
            set => _source = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the destination
        /// </summary>
        public string Destination
        {
            get => _destination;
            set => _destination = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the ssrc
        /// </summary>
        public string SSRC
        {
            get => _ssrc;
            set => _ssrc = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the mode
        /// </summary>
        public string Mode
        {
            get => _mode;
            set => _mode = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the client ports
        /// </summary>
        public RtspPortPair ClientPort
        {
            get => _clientPort;
            set => _clientPort = value;
        }

        /// <summary>
        /// Gets / Sets the server ports
        /// </summary>
        public RtspPortPair ServerPort
        {
            get => _serverPort;
            set => _serverPort = value;
        }

        /// <summary>
        /// Gets / Sets the interleaved ports
        /// </summary>
        public RtspPortPair InterleavedPort
        {
            get => _interleavedPort;
            set => _interleavedPort = value;
        }

        /// <summary>
        /// Gets / Sets the port
        /// </summary>
        public RtspPortPair Port
        {
            get => _port;
            set => _port = value;
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _transportType    != RtspTransportType.Unknown  
                && _transmissionType != RtspTransmissionType.Unknown;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            if ( _transportType == RtspTransportType.Unknown || _transmissionType == RtspTransmissionType.Unknown )
            {
                return string.Empty;
            }

            var writer = new RtspHeaderWriter( RtspSeparator.SemiColon , RtspOperator.Equality );

            writer.Write( RtspDataConverter.ConvertToString( _transportType ) );
            writer.WriteSeparator();

            writer.Write( RtspDataConverter.ConvertToString( _transmissionType ) );

            if ( !string.IsNullOrWhiteSpace( _destination ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.Destination , _destination );
            }

            if ( RtspPortPair.IsNotNull( _interleavedPort ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.Interleaved , _interleavedPort.ToString() );
            }

            if ( 0 < _ttl )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.TTL , _ttl );
            }

            if ( 0 < _layers )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.Layers , _layers );
            }

            if ( RtspPortPair.IsNotNull( _port ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.Port , _port.ToString() );
            }

            if ( RtspPortPair.IsNotNull( _clientPort ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.ClientPort , _clientPort.ToString() );
            }

            if ( RtspPortPair.IsNotNull( _serverPort ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.ServerPort , _serverPort.ToString() );
            }

            if ( !string.IsNullOrWhiteSpace( _ssrc ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.SSRC , _ssrc );
            }

            if ( !string.IsNullOrWhiteSpace( _mode ) )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.Mode , _mode );
            }

            return writer.Output;
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderTransport result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.SemiColon );

            if ( ! parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RtspHeaderReader( parser.GetParsedHeaders() ) )
            {
                reader.Operator = RtspOperator.Equality;
                reader.IncludeQuotes = true;

                result = new RtspHeaderTransport();

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
                            result.InterleavedPort = RtspPortPair.TryParse( reader.GetElementValue() , out RtspPortPair port ) ? port : RtspPortPair.Zero;
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
                            result.Port = RtspPortPair.TryParse( reader.GetElementValue() , out RtspPortPair port ) ? port : RtspPortPair.Zero;
                        }

                        if ( reader.IsClientPortElementType )
                        {
                            result.ClientPort = RtspPortPair.TryParse( reader.GetElementValue() , out RtspPortPair port ) ? port : RtspPortPair.Zero;
                        }

                        if ( reader.IsServerPortElementType )
                        {
                            result.ServerPort = RtspPortPair.TryParse( reader.GetElementValue() , out RtspPortPair port ) ? port : RtspPortPair.Zero;
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
                            result.TransportType = RtspTransportType.RTP_AVP_UDP;
                        }

                        if ( reader.IsRtpAvpTcpElementType )
                        {
                            result.TransportType = RtspTransportType.RTP_AVP_TCP;
                        }

                        if ( reader.IsMulticastElementType )
                        {
                            result.TransmissionType = RtspTransmissionType.Multicast;
                        }

                        if ( reader.IsUnicastElementType )
                        {
                            result.TransmissionType = RtspTransmissionType.Unicast;
                        }

                        #endregion
                    }
                }

                return true;
            }
        }
    }
}
