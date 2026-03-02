using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class TransportRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Transport";

        



        
        private readonly HashSet<string> _extensions = new HashSet<string>();






        public string Transport { get; private set; } = string.Empty;

        public string Transmission { get; private set; } = string.Empty;

        public string Source { get; private set; } = string.Empty;

        public string Destination { get; private set; } = string.Empty;

        public string Address { get; private set; } = string.Empty;

        public string Host { get; private set; } = string.Empty;

        public string SSRC { get; private set; } = string.Empty;

        public string Mode { get; private set; } = string.Empty;

        public byte? TTL { get; set; }

        public int? Layers { get; set; }
        
        public ValueRange? Port { get; set; }
        
        public ValueRange? ClientPort { get; set; }
        
        public ValueRange? ServerPort { get; set; }
        
        public ValueRange? Interleaved { get; set; }
        
        public IReadOnlyCollection<string> Extensions { get => _extensions; }
        






        public void SetTransport( string value )
        {
            Transport = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetTransmission( string value )
        {
            Transmission = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetSource( string value )
        {
            Source = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetDestination( string value )
        {
            Destination = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetAddress( string value )
        {
            Address = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetHost( string value )
        {
            Host = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetSSRC( string value )
        {
            SSRC = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetMode( string value )
        {
            Mode = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetTTL( string value )
        {
            TTL = byte.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? new byte?( result )
                : null
                ;
        }

        public void SetLayers( string value )
        {
            Layers = byte.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? new byte?( result )
                : null
                ;
        }

        public void SetPortRange( string value )
        {
            Port = ValueRange.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var range )
                ? new ValueRange?( range )
                : null
                ;
        }
        
        public void SetClientPortRange( string value )
        {
            ClientPort = ValueRange.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var range )
                ? new ValueRange?( range )
                : null
                ;
        }
        
        public void SetServerPortRange( string value )
        {
            ServerPort = ValueRange.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var range )
                ? new ValueRange?( range )
                : null
                ;
        }

        public void SetInterleavedRange( string value )
        {
            Interleaved = ValueRange.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var range )
                ? new ValueRange?( range )
                : null
                ;
        }

        public bool AddExtension( string value )
        {
            var text = RtspHeaderParser.Formatter.Filter( value );

            if ( string.IsNullOrWhiteSpace( text ) )
            {
                return false;
            }

            return _extensions.Add( text );
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( RtspHeaderParser.Formatter.Filter( value ) );
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

            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , ";" , out var tokens ) )
            {
                var header = new TransportRtspHeader();

                var comparer = StringComparer.OrdinalIgnoreCase;
                
                foreach ( var token in tokens )
                {
                    if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                    {
                        if ( comparer.Equals( "destination" , parameter.Name ) )
                        {
                            header.SetDestination( parameter.Value );
                        }
                        else if ( comparer.Equals( "source" , parameter.Name ) )
                        {
                            header.SetSource( parameter.Value );
                        }
                        else if ( comparer.Equals( "address" , parameter.Name ) )
                        {
                            header.SetAddress( parameter.Value );
                        }
                        else if ( comparer.Equals( "host" , parameter.Name ) )
                        {
                            header.SetHost( parameter.Value );
                        }
                        else if ( comparer.Equals( "ssrc" , parameter.Name ) )
                        {
                            header.SetSSRC( parameter.Value );
                        }
                        else if ( comparer.Equals( "mode" , parameter.Name ) )
                        {
                            header.SetMode( parameter.Value );
                        }
                        else if ( comparer.Equals( "layers" , parameter.Name ) )
                        {
                            header.SetLayers( parameter.Value );
                        }
                        else if ( comparer.Equals( "ttl" , parameter.Name ) )
                        {
                            header.SetTTL( parameter.Value );
                        }
                        else if ( comparer.Equals( "port" , parameter.Name ) )
                        {
                            header.SetPortRange( parameter.Value );
                        }
                        else if ( comparer.Equals( "client_port" , parameter.Name ) )
                        {
                            header.SetClientPortRange( parameter.Value );
                        }
                        else if ( comparer.Equals( "server_port" , parameter.Name ) )
                        {
                            header.SetServerPortRange( parameter.Value );
                        }
                        else if ( comparer.Equals( "interleaved" , parameter.Name ) )
                        {
                            header.SetInterleavedRange( parameter.Value );
                        }
                        else if ( Constants.TransportsTypes.Contains( parameter.Name ) )
                        {
                            header.SetTransport( token );
                        }
                        else if ( Constants.TransmissionsTypes.Contains( parameter.Name ) )
                        {
                            header.SetTransmission( token );
                        }
                        else
                        {
                            header.AddExtension( token );
                        }
                    }
                    else
                    {
                        if ( Constants.TransportsTypes.Contains( token ) )
                        {
                            header.SetTransport( token );
                        }
                        else if ( Constants.TransmissionsTypes.Contains( token ) )
                        {
                            header.SetTransmission( token );
                        }
                        else    
                        {
                            header.AddExtension( token );
                        }
                    }
                }
                
                if ( string.IsNullOrWhiteSpace( header.Transport ) ||  string.IsNullOrWhiteSpace( header.Transmission ) )
                {
                    return false;
                }
                
                result = header;
            }

            return result != null;
        }
    }
}
