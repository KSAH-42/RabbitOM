using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Filters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;

    public sealed class TransportRtspHeader
    {
        public static readonly string TypeName = "Transport";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringRtspHeaderFilter ValueFilter = StringRtspHeaderFilter.UnQuoteFilter;
        public static readonly StringRtspHeaderValidator ValueValidator = StringRtspHeaderValidator.TokenValidator;


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
        private readonly StringRtspHashSet _extensions = new StringRtspHashSet();



        public string Transport 
        { 
            get => _transport; 
            set => _transport = ValueFilter.Filter( value ); 
        }

        public string Transmission
        { 
            get => _transmission; 
            set => _transmission = ValueFilter.Filter( value ); 
        }

        public string Source
        { 
            get => _source; 
            set => _source = ValueFilter.Filter( value ); 
        }

        public string Destination
        { 
            get => _destination; 
            set => _destination = ValueFilter.Filter( value ); 
        }

        public string Address
        { 
            get => _address; 
            set => _address = ValueFilter.Filter( value ); 
        }

        public string Host
        { 
            get => _host; 
            set => _host = ValueFilter.Filter( value ); 
        }

        public string SSRC
        { 
            get => _ssrc; 
            set => _ssrc = ValueFilter.Filter( value ); 
        }

        public string Mode
        { 
            get => _mode; 
            set => _mode = ValueFilter.Filter( value ); 
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
        


        
        public bool AddExtension( string value )
        {
            if ( ValueValidator.TryValidate( value ) )
            {
                return _extensions.Add( ValueFilter.Filter( value ) );
            }

            return false;
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( ValueFilter.Filter( value ) );
        }

        public void ClearExtensions()
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







        public static bool TryParse( string input , out TransportRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out var tokens ) )
            {
                var header = new TransportRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                    {
                        if ( ValueComparer.Equals( "destination" , parameter.Name ) )
                        {
                            header.Destination = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "source" , parameter.Name ) )
                        {
                            header.Source = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "address" , parameter.Name ) )
                        {
                            header.Address = parameter.Value ;
                        }
                        else if ( ValueComparer.Equals( "host" , parameter.Name ) )
                        {
                            header.Host = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "ssrc" , parameter.Name ) )
                        {
                            header.SSRC = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "mode" , parameter.Name ) )
                        {
                            header.Mode = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "layers" , parameter.Name ) )
                        {
                            if ( int.TryParse( ValueFilter.Filter( parameter.Value ) , out int value ) )
                            {
                                header.Layers = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "ttl" , parameter.Name ) )
                        {
                            if ( byte.TryParse( ValueFilter.Filter( parameter.Value ) , out byte value ) )
                            {
                                header.TTL = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "port" , parameter.Name ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header.Port = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "client_port" , parameter.Name ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header.ClientPort = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "server_port" , parameter.Name ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header.ServerPort = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "interleaved" , parameter.Name ) )
                        {
                            if ( ValueRange.TryParse( parameter.Value , out var value ) )
                            {
                                header.Interleaved = value;
                            }
                        }
                        else if ( SupportedTypes.Transports.Contains( parameter.Name ) )
                        {
                            header.Transport = token;
                        }
                        else if ( SupportedTypes.Transmissions.Contains( parameter.Name ) )
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
                
                if ( ValueValidator.TryValidate( header.Transport ) && ValueValidator.TryValidate( header.Transmission ) )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
