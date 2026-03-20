using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    
    public sealed class TransportRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Transport";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
       

        private string _transport = string.Empty;
        private string _transmission = string.Empty;
        private string _source = string.Empty;
        private string _destination = string.Empty;
        private string _address = string.Empty;
        private string _host = string.Empty;
        private string _ssrc = string.Empty;
        private string _mode = string.Empty;
        private byte? _ttl;
        private int? _layers;
        private ValueRange? _port;
        private ValueRange? _clientPort;
        private ValueRange? _serverPort;
        private ValueRange? _interleaved;
        private readonly HashSet<string> _extensions = new HashSet<string>( StringComparer.OrdinalIgnoreCase );



        public string Transport 
        { 
            get => _transport; 
            set => _transport = ValueAdapter.Adapt( value ); 
        }

        public string Transmission
        { 
            get => _transmission; 
            set => _transmission = ValueAdapter.Adapt( value ); 
        }

        public string Source
        { 
            get => _source; 
            set => _source = ValueAdapter.Adapt( value ); 
        }

        public string Destination
        { 
            get => _destination; 
            set => _destination = ValueAdapter.Adapt( value ); 
        }

        public string Address
        { 
            get => _address; 
            set => _address = ValueAdapter.Adapt( value ); 
        }

        public string Host
        { 
            get => _host; 
            set => _host = ValueAdapter.Adapt( value ); 
        }

        public string SSRC
        { 
            get => _ssrc; 
            set => _ssrc = ValueAdapter.Adapt( value ); 
        }

        public string Mode
        { 
            get => _mode; 
            set => _mode = ValueAdapter.Adapt( value ); 
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
        
        public IReadOnlyCollection<string> Extensions
        {
            get => _extensions;
        }
        



        public static bool TryParse( string input , out TransportRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var header = new TransportRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "destination" , parameter.Key ) )
                        {
                            header.Destination = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "source" , parameter.Key ) )
                        {
                            header.Source = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "address" , parameter.Key ) )
                        {
                            header.Address = parameter.Value ;
                        }
                        else if ( ValueComparer.Equals( "host" , parameter.Key ) )
                        {
                            header.Host = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "ssrc" , parameter.Key ) )
                        {
                            header.SSRC = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "mode" , parameter.Key ) )
                        {
                            header.Mode = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "layers" , parameter.Key ) )
                        {
                            if ( int.TryParse( ValueAdapter.Adapt( parameter.Value ) , out int value ) )
                            {
                                header.Layers = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "ttl" , parameter.Key ) )
                        {
                            if ( byte.TryParse( ValueAdapter.Adapt( parameter.Value ) , out byte value ) )
                            {
                                header.TTL = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "port" , parameter.Key ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header.Port = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "client_port" , parameter.Key ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header.ClientPort = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "server_port" , parameter.Key ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header.ServerPort = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "interleaved" , parameter.Key ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header.Interleaved = value;
                            }
                        }
                        else if ( SupportedTypes.Transports.Contains( parameter.Key ) )
                        {
                            header.Transport = token;
                        }
                        else if ( SupportedTypes.Transmissions.Contains( parameter.Key ) )
                        {
                            header.Transmission = token;
                        }
                        else
                        {
                            header.AddExtension( token );
                        }
                    }
                    else
                    {
                        if ( SupportedTypes.Transports.Contains( token ) )
                        {
                            header.Transport = token;
                        }
                        else if ( SupportedTypes.Transmissions.Contains( token ) )
                        {
                            header.Transmission = token;
                        }
                        else    
                        {
                            header.AddExtension( token );
                        }
                    }
                }
                
                if ( RtspHeaderValueValidator.IsValidToken( header.Transport ) && RtspHeaderValueValidator.IsValidToken( header.Transmission ) )
                {
                    result = header;
                }
            }

            return result != null;
        }

        


        public bool AddExtension( string value )
        {
            if ( RtspHeaderValueValidator.IsValid( value = ValueAdapter.Adapt( value ) ) )
            {
                return _extensions.Add( value );
            }

            return false;
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( ValueAdapter.Adapt( value ) );
        }

        public void RemoveExtensions()
        {
            _extensions.Clear();
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

            foreach ( var extension in _extensions )
            {
                builder.AppendFormat( "{0};" , extension );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }
    }
}
