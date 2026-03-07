using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;

    public sealed class ConferenceRtspHeader
    {
        public static readonly string TypeName = "Conference";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.UnQuoteAdapter;
        public static readonly StringRtspHeaderValidator ValueValidator = StringRtspHeaderValidator.TokenValidator;




        private string _conferenceId = string.Empty;
        private string _transport = string.Empty;
        private string _transmission = string.Empty;
        private string _source = string.Empty;
        private string _destination = string.Empty;
        private string _address = string.Empty;
        private string _host = string.Empty;
        private string _role = string.Empty;
        private string _mode = string.Empty;
        private string _tag = string.Empty;
        private string _session = string.Empty;
        private string _access = string.Empty;
        private byte? _ttl;
        private ValueRange? _port;
        private readonly StringRtspHashSet _extensions = new StringRtspHashSet();




        public string ConferenceId
        {
            get => _conferenceId;
            set => _conferenceId = ValueAdapter.Adapt( value );
        }

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

        public string Role
        {
            get => _role;
            set => _role = ValueAdapter.Adapt( value );
        }

        public string Mode
        {
            get => _mode;
            set => _mode = ValueAdapter.Adapt( value );
        }

        public string Tag
        {
            get => _tag;
            set => _tag = ValueAdapter.Adapt( value );
        }

        public string Session
        {
            get => _session;
            set => _session = ValueAdapter.Adapt( value );
        }

        public string Access
        {
            get => _access;
            set => _access = ValueAdapter.Adapt( value );
        }

        public byte? TTL
        {
            get => _ttl;
            set => _ttl = value;
        }

        public ValueRange? Port
        {
            get => _port;
            set => _port = value;
        }
        
        public IReadOnlyCollection<string> Extensions
        {
            get => _extensions;
        }
        








        public bool AddExtension( string value )
        {
            return _extensions.Add( ValueAdapter.Adapt( value ) );
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( ValueAdapter.Adapt( value ) );
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
                builder.AppendFormat( "{0}; " , ConferenceId );
            }

            if ( ! string.IsNullOrWhiteSpace( Transport ) )
            {
                builder.AppendFormat( "{0}; " , Transport );
            }

            if ( ! string.IsNullOrWhiteSpace( Transmission ) )
            {
                builder.AppendFormat( "{0}; " , Transmission );
            }

            if ( ! string.IsNullOrWhiteSpace( Source ) )
            {
                builder.AppendFormat( "source={0}; " , Source );
            }

            if ( ! string.IsNullOrWhiteSpace( Destination ) )
            {
                builder.AppendFormat( "destination={0}; " , Destination );
            }

            if ( ! string.IsNullOrWhiteSpace( Address ) )
            {
                builder.AppendFormat( "address={0}; " , Address );
            }

            if ( ! string.IsNullOrWhiteSpace( Host ) )
            {
                builder.AppendFormat( "host={0}; " , Host );
            }

            if ( ! string.IsNullOrWhiteSpace( Role ) )
            {
                builder.AppendFormat( "role={0}; " , Role );
            }

            if ( ! string.IsNullOrWhiteSpace( Mode ) )
            {
                builder.AppendFormat( "mode={0};" , Mode );
            }

            if ( ! string.IsNullOrWhiteSpace( Tag ) )
            {
                builder.AppendFormat( "tag={0}; " , Tag );
            }

            if ( ! string.IsNullOrWhiteSpace( Session ) )
            {
                builder.AppendFormat( "session={0}; " , Session);
            }

            if ( ! string.IsNullOrWhiteSpace( Access ) )
            {
                builder.AppendFormat( "access={0}; " , Session);
            }

            if ( TTL.HasValue )
            {
                builder.AppendFormat( "ttl={0}; " , TTL );
            }

            if ( Port.HasValue )
            {
                builder.AppendFormat( "port={0}; " , Port );
            }

            foreach ( var extension in _extensions )
            {
                builder.AppendFormat( "{0}; " , extension );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }









        public static bool TryParse( string input , out ConferenceRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out var tokens ) )
            {
                var header = new ConferenceRtspHeader();

                foreach ( var token in tokens.Where( ValueValidator.TryValidate ) )
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
                            header.Address = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "host" , parameter.Name ) )
                        {
                            header.Host = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "role" , parameter.Name ) )
                        {
                            header.Role = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "mode" , parameter.Name ) )
                        {
                            header.Mode = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "tag" , parameter.Name ) )
                        {
                            header.Tag = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "session" , parameter.Name ) )
                        {
                            header.Session = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "access" , parameter.Name ) )
                        {
                            header.Access = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "ttl" , parameter.Name ) )
                        {
                            if ( byte.TryParse( ValueAdapter.Adapt( parameter.Value ) , out byte value ) )
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
                            if ( ! string.IsNullOrWhiteSpace( header.ConferenceId ) )
                            {
                                header.AddExtension( token );
                            }
                            else
                            {
                                header.ConferenceId = token;
                            }
                        }
                    }
                }
                
                if ( ValueValidator.TryValidate( header.ConferenceId ) )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
