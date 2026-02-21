using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class TransportRtspHeader
    {
        public static readonly string TypeName = "Transport";






        public string Transport { get; private set; } = string.Empty;

        public string Transmission { get; private set; } = string.Empty;

        public string Source { get; private set; } = string.Empty;

        public string Destination { get; private set; } = string.Empty;

        public string SSRC { get; private set; } = string.Empty;

        public string Mode { get; private set; } = string.Empty;

        public byte? TTL { get; set; }

        public int? Layers { get; set; }
        
        public int? ClientPortStart { get; set; }
        
        public int? ClientPortEnd { get; set; }
        
        public int? ServerPortStart { get; set; }
        
        public int? ServerPortEnd { get; set; }

        public int? PortStart { get; set; }
        
        public int? PortEnd { get; set; }

        public byte? InterleavedStart { get; set; }
        
        public byte? InterleavedEnd { get; set; }

        





        public static bool TryParse( string input , out TransportRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ';' , out var tokens ) )
            {
                var header = new TransportRtspHeader();

                header.SetTransport( tokens.Where( token => ! token.Contains( "=" ) ).FirstOrDefault() );
                
                if ( string.IsNullOrWhiteSpace( header.Transport ) )
                {
                    return false;
                }

                header.SetTransmission( tokens.Where( token => ! token.Contains( "=" ) ).Skip(1).FirstOrDefault() );

                if ( string.IsNullOrWhiteSpace( header.Transmission ) )
                {
                    return false;
                }

                foreach ( var token in tokens )
                {
                    if ( StringParameterRtspHeaderParser.TryParse( token , '=' , out var parameter ) )
                    {
                        if ( string.Equals( "destination" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetDestination( parameter.Value );
                        }
                        else if ( string.Equals( "source" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetSource( parameter.Value );
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
                    }
                }
                
                result = header;
            }

            return result != null;
        }







        public void SetTransport( string value )
        {
            Transport = RtspValueNormalizer.Normalize( value );
        }

        public void SetTransmission( string value )
        {
            Transmission = RtspValueNormalizer.Normalize( value );
        }

        public void SetSource( string value )
        {
            Source = RtspValueNormalizer.Normalize( value );
        }

        public void SetDestination( string value )
        {
            Destination = RtspValueNormalizer.Normalize( value );
        }

        public void SetSSRC( string value )
        {
            SSRC = RtspValueNormalizer.Normalize( value );
        }

        public void SetMode( string value )
        {
            Mode = RtspValueNormalizer.Normalize( value );
        }

        public void SetTTL( string value )
        {
            TTL = null;

            if ( byte.TryParse( RtspValueNormalizer.Normalize( value ) , out var result ) )
            {
                TTL = result;
            }
        }

        public void SetLayers( string value )
        {
            Layers = null;

            if ( int.TryParse( RtspValueNormalizer.Normalize( value ) , out var result ) )
            {
                Layers = result;
            }
        }
        
        public void SetClientPortRange( string value )
        {
            ClientPortStart = null;
            ClientPortEnd = null;

            if ( StringParameterRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( value ) , '-' , out var range ) )
            {
                if ( int.TryParse( range.Name , out var port ) )
                {
                    ClientPortStart = port;
                }

                if ( int.TryParse( range.Value , out port ) )
                {
                    ClientPortEnd = port;
                }
            }
        }
        
        public void SetServerPortRange( string value )
        {
            ServerPortStart = null;
            ServerPortEnd = null;

            if ( StringParameterRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( value ) , '-' , out var range ) )
            {
                if ( int.TryParse( range.Name , out var port ) )
                {
                    ServerPortStart = port;
                }

                if ( int.TryParse( range.Value , out port ) )
                {
                    ServerPortEnd = port;
                }
            }
        }

        public void SetPortRange( string value )
        {
            PortStart = null;
            PortEnd = null;

            if ( StringParameterRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( value ) , '-' , out var range ) )
            {
                if ( int.TryParse( range.Name , out var port ) )
                {
                    PortStart = port;
                }

                if ( int.TryParse( range.Value , out port ) )
                {
                    PortEnd = port;
                }
            }
        }

        public void SetInterleavedRange( string value )
        {
            InterleavedStart = null;
            InterleavedEnd = null;

            if ( StringParameterRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( value ) , '-' , out var range ) )
            {
                if ( byte.TryParse( range.Name , out var port ) )
                {
                    InterleavedStart = port;
                }

                if ( byte.TryParse( range.Value , out port ) )
                {
                    InterleavedEnd = port;
                }
            }
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

            if ( ClientPortStart.HasValue && ClientPortEnd.HasValue )
            {
                builder.AppendFormat( "client_port={0}-{1};" , ClientPortStart , ClientPortEnd );
            }

            if ( ServerPortStart.HasValue && ServerPortEnd.HasValue )
            {
                builder.AppendFormat( "server_port={0}-{1};" , ServerPortStart , ServerPortEnd );
            }

            if ( PortStart.HasValue && PortEnd.HasValue )
            {
                builder.AppendFormat( "port={0}-{1};" , PortStart , PortEnd );
            }

            if ( InterleavedStart.HasValue && InterleavedEnd.HasValue )
            {
                builder.AppendFormat( "interleaved={0}-{1};" , InterleavedStart , InterleavedEnd );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }
    }
}
