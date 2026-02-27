using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class TransportRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Transport";

        public static readonly IReadOnlyCollection<string> SupportedTransports = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            "RTP",
            "RTP/AVP",
            "RTP/AVP/TCP",
            "RTP/AVP/UDP",
            "RTP/AVPF",
            "RTP/AVPF/TCP",
            "RTP/AVPF/UDP",
            "RTP/SAVP",
            "RTP/SAVP/TCP",
            "RTP/SAVP/UDP",
            "RTP/SAVPF",
            "RTP/SAVPF/TCP",
            "RTP/SAVPF/UDP",
            "SRTP",
            "SRTP/AVP",
            "SRTP/AVP/TCP",
            "SRTP/AVP/UDP",
            "SRTP/AVPF",
            "SRTP/AVPF/TCP",
            "SRTP/AVPF/UDP",
            "SRTP/SAVP",
            "SRTP/SAVP/TCP",
            "SRTP/SAVP/UDP",
            "SRTP/SAVPF",
            "SRTP/SAVPF/TCP",
            "SRTP/SAVPF/UDP",
        };

        public static readonly IReadOnlyCollection<string> SupportedTransmissions = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            "unicast",
            "multicast",
        };

        



        
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
        
        public PortRange? Port { get; set; }
        
        public PortRange? ClientPort { get; set; }
        
        public PortRange? ServerPort { get; set; }
        
        public InterleavedRange? Interleaved { get; set; }
        
        public IReadOnlyCollection<string> Extensions { get => _extensions; }
        






        public void SetTransport( string value )
        {
            Transport = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetTransmission( string value )
        {
            Transmission = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetSource( string value )
        {
            Source = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetDestination( string value )
        {
            Destination = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetAddress( string value )
        {
            Address = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetHost( string value )
        {
            Host = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetSSRC( string value )
        {
            SSRC = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetMode( string value )
        {
            Mode = StringRtspHeaderNormalizer.Normalize( value );
        }

        public void SetTTL( string value )
        {
            TTL = byte.TryParse( StringRtspHeaderNormalizer.Normalize( value ) , out var result )
                ? new byte?( result )
                : null
                ;
        }

        public void SetLayers( string value )
        {
            Layers = byte.TryParse( StringRtspHeaderNormalizer.Normalize( value ) , out var result )
                ? new byte?( result )
                : null
                ;
        }

        public void SetPortRange( string value )
        {
            Port = PortRange.TryParse( StringRtspHeaderNormalizer.Normalize( value ) , out var range )
                ? new PortRange?( range )
                : null
                ;
        }
        
        public void SetClientPortRange( string value )
        {
            ClientPort = PortRange.TryParse( StringRtspHeaderNormalizer.Normalize( value ) , out var range )
                ? new PortRange?( range )
                : null
                ;
        }
        
        public void SetServerPortRange( string value )
        {
            ServerPort = PortRange.TryParse( StringRtspHeaderNormalizer.Normalize( value ) , out var range )
                ? new PortRange?( range )
                : null
                ;
        }

        public void SetInterleavedRange( string value )
        {
            Interleaved = InterleavedRange.TryParse( StringRtspHeaderNormalizer.Normalize( value ) , out var range )
                ? new InterleavedRange?( range )
                : null
                ;
        }

        public bool AddExtension( string value )
        {
            var text = StringRtspHeaderNormalizer.Normalize( value );

            if ( string.IsNullOrWhiteSpace( text ) )
            {
                return false;
            }

            return _extensions.Add( text );
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( StringRtspHeaderNormalizer.Normalize( value ) );
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







        public static bool TryParse( string input , out TransportRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , ";" , out var tokens ) )
            {
                var header = new TransportRtspHeaderValue();
                
                foreach ( var token in tokens )
                {
                    if ( StringParameter.TryParse( token , "=" , out var parameter ) )
                    {
                        if ( string.Equals( "destination" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetDestination( parameter.Value );
                        }
                        else if ( string.Equals( "source" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetSource( parameter.Value );
                        }
                        else if ( string.Equals( "address" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetAddress( parameter.Value );
                        }
                        else if ( string.Equals( "host" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetHost( parameter.Value );
                        }
                        else if ( string.Equals( "ssrc" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetSSRC( parameter.Value );
                        }
                        else if ( string.Equals( "mode" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetMode( parameter.Value );
                        }
                        else if ( string.Equals( "layers" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetLayers( parameter.Value );
                        }
                        else if ( string.Equals( "ttl" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetTTL( parameter.Value );
                        }
                        else if ( string.Equals( "port" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetPortRange( parameter.Value );
                        }
                        else if ( string.Equals( "client_port" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetClientPortRange( parameter.Value );
                        }
                        else if ( string.Equals( "server_port" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetServerPortRange( parameter.Value );
                        }
                        else if ( string.Equals( "interleaved" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetInterleavedRange( parameter.Value );
                        }
                        else if ( SupportedTransports.Contains( parameter.Name ) )
                        {
                            header.SetTransport( token );
                        }
                        else if ( SupportedTransmissions.Contains( parameter.Name ) )
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
                        if ( SupportedTransports.Contains( token ) )
                        {
                            header.SetTransport( token );
                        }
                        else if ( SupportedTransmissions.Contains( token ) )
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
