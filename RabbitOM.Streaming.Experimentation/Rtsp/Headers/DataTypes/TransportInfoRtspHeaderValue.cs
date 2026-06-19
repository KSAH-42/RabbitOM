using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class TransportInfoRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        
        private string _transport = string.Empty;
        private string _transmission = string.Empty;
        private string _source = string.Empty;
        private string _destination = string.Empty;
        private string _address = string.Empty;
        private string _host = string.Empty;
        private string _ssrc = string.Empty;
        private string _mode = string.Empty;
        private string _append = string.Empty;
        private byte? _ttl;
        private int? _layers;
        private ValueRange? _port;
        private ValueRange? _clientPort;
        private ValueRange? _serverPort;
        private ValueRange? _interleaved;



        public string Transport
        { 
            get => _transport;
            set => _transport = EnsureValue( value );
        }

        public string Transmission
        { 
            get => _transmission;
            set => _transmission = EnsureValue( value );
        }

        public string Source
        { 
            get => _source;
            set => _source = EnsureValue( value );
        }

        public string Destination
        { 
            get => _destination;
            set => _destination = EnsureValue( value );
        }

        public string Address
        { 
            get => _address;
            set => _address = EnsureValue( value );
        }

        public string Host
        { 
            get => _host;
            set => _host = EnsureValue( value );
        }

        public string SSRC
        { 
            get => _ssrc;
            set => _ssrc = EnsureValue( value );
        }

        public string Mode
        { 
            get => _mode;
            set => _mode = EnsureValue( value );
        }

        public string Append
        { 
            get => _append;
            set => _append = EnsureValue( value );
        }

        public byte? TTL
        {
            get => _ttl;
            set => _ttl = value;
        }

        public int? Layers
        {
            get => _layers;
            set => _layers = value;
        }
        
        public ValueRange? Port
        {
            get => _port;
            set => _port = value;
        }
        
        public ValueRange? ClientPort
        {
            get => _clientPort;
            set => _clientPort = value;
        }
        
        public ValueRange? ServerPort
        {
            get => _serverPort;
            set => _serverPort = value;
        }
        
        public ValueRange? Interleaved
        {
            get => _interleaved;
            set => _interleaved = value;
        }




        private static string EnsureValue( string value )
        {
            return RtspHeaderValueValidator.EnsureWellFormed( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        private static bool IsWellFormedValue( string value )
        {
            return RtspHeaderValueValidator.IsWellFormed( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }
        public static bool TryParse( string input , out TransportInfoRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var header = new TransportInfoRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "destination" , parameter.Key ) )
                        {
                            header._destination = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "source" , parameter.Key ) )
                        {
                            header._source = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "address" , parameter.Key ) )
                        {
                            header._address = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "host" , parameter.Key ) )
                        {
                            header._host = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "ssrc" , parameter.Key ) )
                        {
                            header._ssrc = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "mode" , parameter.Key ) )
                        {
                            header._mode = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "append" , parameter.Key ) )
                        {
                            header._append = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "layers" , parameter.Key ) )
                        {
                            if ( int.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value ) , out int value ) )
                            {
                                header._layers = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "ttl" , parameter.Key ) )
                        {
                            if ( byte.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value ) , out byte value ) )
                            {
                                header._ttl = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "port" , parameter.Key ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header._port = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "client_port" , parameter.Key ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header._clientPort = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "server_port" , parameter.Key ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header._serverPort = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "interleaved" , parameter.Key ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header._interleaved = value;
                            }
                        }
                        else if ( SupportedTypes.IsTransportSupported( parameter.Key ) )
                        {
                            header._transport = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( SupportedTypes.IsTransmissionSupported( parameter.Key ) )
                        {
                            header._transmission = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                    }
                    else
                    {
                        if ( SupportedTypes.IsTransportSupported( token ) )
                        {
                            header._transport = RtspHeaderValueSanitizer.UnQuotesWithTrim( token );
                        }
                        else if ( SupportedTypes.IsTransmissionSupported( token ) )
                        {
                            header._transmission = RtspHeaderValueSanitizer.UnQuotesWithTrim( token );
                        }
                    }
                }
                
                if ( IsWellFormedValue( header.Transport ) && IsWellFormedValue( header.Transmission ) )
                {
                    result = header;
                }
            }

            return result != null;
        }

        



        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( ! string.IsNullOrWhiteSpace( Transport ) )
            {
                builder.AppendFormat( "{0};" , Transport );
            }

            if ( ! string.IsNullOrWhiteSpace( Transmission ) )
            {
                builder.AppendFormat( "{0};" , Transmission );
            }

            if ( ! string.IsNullOrWhiteSpace( Source ) )
            {
                builder.AppendFormat( "source={0};" , Source );
            }

            if ( ! string.IsNullOrWhiteSpace( Destination ) )
            {
                builder.AppendFormat( "destination={0};" , Destination );
            }

            if ( ! string.IsNullOrWhiteSpace( Address ) )
            {
                builder.AppendFormat( "address={0};" , Address );
            }

            if ( ! string.IsNullOrWhiteSpace( Host ) )
            {
                builder.AppendFormat( "host={0};" , Host );
            }

            if ( ! string.IsNullOrWhiteSpace( SSRC ) )
            {
                builder.AppendFormat( "ssrc={0};" , SSRC );
            }

            if ( ! string.IsNullOrWhiteSpace( Mode ) )
            {
                builder.AppendFormat( "mode=\"{0}\";" , Mode );
            }

            if ( ! string.IsNullOrWhiteSpace( Append ) )
            {
                builder.AppendFormat( "append=\"{0}\";" , Append );
            }

            if ( TTL.HasValue )
            {
                builder.AppendFormat( "ttl={0};" , TTL );
            }

            if ( Layers.HasValue )
            {
                builder.AppendFormat( "layers={0};" , Layers );
            }

            if ( Port.HasValue )
            {
                builder.AppendFormat( "port={0};" , Port );
            }

            if ( ClientPort.HasValue )
            {
                builder.AppendFormat( "client_port={0};" , ClientPort );
            }

            if ( ServerPort.HasValue )
            {
                builder.AppendFormat( "server_port={0};" , ServerPort );
            }

            if ( Interleaved.HasValue )
            {
                builder.AppendFormat( "interleaved={0};" , Interleaved );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }
    }
}
