using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class ConferenceRtspHeader
    {
        public static readonly string TypeName = "Conference";

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

        private static readonly IReadOnlyCollection<string> SupportedConferenceIdsNames = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
        {
            "id",
            "identifier",
            "conference-id",
            "conference_id",
            "conf-id",
            "conf_id"
        };

        



        
        private readonly HashSet<string> _extensions = new HashSet<string>();





        public string ConferenceId { get; private set; } = string.Empty;

        public string Transport { get; private set; } = string.Empty;

        public string Transmission { get; private set; } = string.Empty;

        public string Source { get; private set; } = string.Empty;

        public string Destination { get; private set; } = string.Empty;

        public string Address { get; private set; } = string.Empty;

        public string Host { get; private set; } = string.Empty;

        public string Role { get; private set; } = string.Empty;

        public string Mode { get; private set; } = string.Empty;

        public string Tag { get; private set; } = string.Empty;

        public string Session { get; private set; } = string.Empty;

        public byte? TTL { get; set; }

        public PortRange? Port { get; set; }
        
        public IReadOnlyCollection<string> Extensions { get => _extensions; }
        






        public void SetConferenceId( string value )
        {
            ConferenceId = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetTransport( string value )
        {
            Transport = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetTransmission( string value )
        {
            Transmission = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetSource( string value )
        {
            Source = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetDestination( string value )
        {
            Destination = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetAddress( string value )
        {
            Address = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetHost( string value )
        {
            Host = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetRole( string value )
        {
            Role = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetMode( string value )
        {
            Mode = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetTag( string value )
        {
            Tag = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetSession( string value )
        {
            Session = RtspHeaderValueNormalizer.Normalize( value );
        }

        public void SetTTL( string value )
        {
            TTL = byte.TryParse( RtspHeaderValueNormalizer.Normalize( value ) , out var result )
                ? new byte?( result )
                : null
                ;
        }

        public void SetPortRange( string value )
        {
            Port = PortRange.TryParse( RtspHeaderValueNormalizer.Normalize( value ) , out var range )
                ? new PortRange?( range )
                : null
                ;
        }
        
        public bool AddExtension( string value )
        {
            var text = RtspHeaderValueNormalizer.Normalize( value );

            if ( string.IsNullOrWhiteSpace( text ) )
            {
                return false;
            }

            return _extensions.Add( text );
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( RtspHeaderValueNormalizer.Normalize( value ) );
        }

        public void RemoveExtensions()
        {
            _extensions.Clear();
        }
        
        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( ! string.IsNullOrWhiteSpace( ConferenceId ) )
            {
                builder.AppendFormat( "{0};" , ConferenceId );
            }

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

            if ( ! string.IsNullOrWhiteSpace( Role ) )
            {
                builder.AppendFormat( "role={0};" , Role );
            }

            if ( ! string.IsNullOrWhiteSpace( Mode ) )
            {
                builder.AppendFormat( "mode={0};" , Mode );
            }

            if ( ! string.IsNullOrWhiteSpace( Tag ) )
            {
                builder.AppendFormat( "tag={0};" , Tag );
            }

            if ( ! string.IsNullOrWhiteSpace( Session ) )
            {
                builder.AppendFormat( "session={0};" , Session);
            }

            if ( TTL.HasValue )
            {
                builder.AppendFormat( "ttl={0};" , TTL );
            }

            if ( Port.HasValue )
            {
                builder.AppendFormat( "port={0};" , Port );
            }

            foreach ( var extension in _extensions )
            {
                builder.AppendFormat( "{0};" , extension );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }





        public static bool IsWellFormedConferenceId( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return value.Any( character => char.IsLetterOrDigit( character ) );
        }

        public static bool TryParse( string input , out ConferenceRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , ";" , out var tokens ) )
            {
                var header = new ConferenceRtspHeader();

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
                        else if ( string.Equals( "role" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetRole( parameter.Value );
                        }
                        else if ( string.Equals( "mode" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetMode( parameter.Value );
                        }
                        else if ( string.Equals( "tag" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetTag( parameter.Value );
                        }
                        else if ( string.Equals( "session" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetSession( parameter.Value );
                        }
                        else if ( string.Equals( "ttl" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetTTL( parameter.Value );
                        }
                        else if ( string.Equals( "port" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetPortRange( parameter.Value );
                        }
                        else if ( SupportedTransports.Contains( parameter.Name ) )
                        {
                            header.SetTransport( token );
                        }
                        else if ( SupportedTransmissions.Contains( parameter.Name ) )
                        {
                            header.SetTransmission( token );
                        }
                        else if ( SupportedConferenceIdsNames.Contains( parameter.Name ) )
                        {
                            header.SetConferenceId( token );
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
                            if ( string.IsNullOrWhiteSpace( header.ConferenceId ) && IsWellFormedConferenceId( token ) )
                            {
                                header.SetConferenceId( token );
                            }
                            else
                            {
                                header.AddExtension( token );
                            }
                        }
                    }
                }
                
                if ( string.IsNullOrWhiteSpace( header.ConferenceId ) )
                {
                    return false;
                }
                
                result = header;
            }

            return result != null;
        }
    }
}
