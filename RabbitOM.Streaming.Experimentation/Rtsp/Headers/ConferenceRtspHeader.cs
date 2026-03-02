using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class ConferenceRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Conference";


        private static IReadOnlyCollection<string> ConferenceIdsPropertiesNames = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
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

        public ValueRange? Port { get; set; }
        
        public IReadOnlyCollection<string> Extensions { get => _extensions; }
        






        public void SetConferenceId( string value )
        {
            ConferenceId = RtspHeaderParser.Formatter.Filter( value );
        }

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

        public void SetRole( string value )
        {
            Role = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetMode( string value )
        {
            Mode = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetTag( string value )
        {
            Tag = RtspHeaderParser.Formatter.Filter( value );
        }

        public void SetSession( string value )
        {
            Session = RtspHeaderParser.Formatter.Filter( value );
        }

        private void SetTTL( string value )
        {
            TTL = byte.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? new byte?( result )
                : null
                ;
        }

        private void SetPortRange( string value )
        {
            Port = ValueRange.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var range )
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

            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , ";" , out var tokens ) )
            {
                var header = new ConferenceRtspHeader();

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
                        else if ( comparer.Equals( "role" , parameter.Name ) )
                        {
                            header.SetRole( parameter.Value );
                        }
                        else if ( comparer.Equals( "mode" , parameter.Name ) )
                        {
                            header.SetMode( parameter.Value );
                        }
                        else if ( comparer.Equals( "tag" , parameter.Name ) )
                        {
                            header.SetTag( parameter.Value );
                        }
                        else if ( comparer.Equals( "session" , parameter.Name ) )
                        {
                            header.SetSession( parameter.Value );
                        }
                        else if ( comparer.Equals( "ttl" , parameter.Name ) )
                        {
                            header.SetTTL( parameter.Value );
                        }
                        else if ( comparer.Equals( "port" , parameter.Name ) )
                        {
                            header.SetPortRange( parameter.Value );
                        }
                        else if ( Constants.TransportsTypes.Contains( parameter.Name ) )
                        {
                            header.SetTransport( token );
                        }
                        else if ( Constants.TransmissionsTypes.Contains( parameter.Name ) )
                        {
                            header.SetTransmission( token );
                        }
                        else if ( ConferenceIdsPropertiesNames.Contains( parameter.Name ) )
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
