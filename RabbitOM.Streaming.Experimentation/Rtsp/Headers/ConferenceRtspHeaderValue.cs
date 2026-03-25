using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;
   
    public sealed class ConferenceRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Conference";

        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
        

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
        private readonly HashSet<string> _extensions = new HashSet<string>( StringComparer.OrdinalIgnoreCase );


        public string ConferenceId
        {
            get => _conferenceId;
            set => _conferenceId = ValueNormalizer.Normalize( value );
        }

        public string Transport
        {
            get => _transport;
            set => _transport = ValueNormalizer.Normalize( value );
        }

        public string Transmission
        {
            get => _transmission;
            set => _transmission = ValueNormalizer.Normalize( value );
        }

        public string Source
        {
            get => _source;
            set => _source = ValueNormalizer.Normalize( value );
        }

        public string Destination
        {
            get => _destination;
            set => _destination = ValueNormalizer.Normalize( value );
        }

        public string Address
        {
            get => _address;
            set => _address = ValueNormalizer.Normalize( value );
        }

        public string Host
        {
            get => _host;
            set => _host = ValueNormalizer.Normalize( value );
        }

        public string Role
        {
            get => _role;
            set => _role = ValueNormalizer.Normalize( value );
        }

        public string Mode
        {
            get => _mode;
            set => _mode = ValueNormalizer.Normalize( value );
        }

        public string Tag
        {
            get => _tag;
            set => _tag = ValueNormalizer.Normalize( value );
        }

        public string Session
        {
            get => _session;
            set => _session = ValueNormalizer.Normalize( value );
        }

        public string Access
        {
            get => _access;
            set => _access = ValueNormalizer.Normalize( value );
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
        

        public static bool TryParse( string input , out ConferenceRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var header = new ConferenceRtspHeaderValue();

                foreach ( var token in tokens.Where( RtspHeaderProtocolValidator.IsValidToken ) )
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
                            header.Address = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "host" , parameter.Key ) )
                        {
                            header.Host = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "role" , parameter.Key ) )
                        {
                            header.Role = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "mode" , parameter.Key ) )
                        {
                            header.Mode = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "tag" , parameter.Key ) )
                        {
                            header.Tag = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "session" , parameter.Key ) )
                        {
                            header.Session = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "access" , parameter.Key ) )
                        {
                            header.Access = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "ttl" , parameter.Key ) )
                        {
                            if ( byte.TryParse( ValueNormalizer.Normalize( parameter.Value ) , out byte value ) )
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
                        else if ( RtspHeaderProtocolValidator.IsValidTransport( parameter.Key ) )
                        {
                            header.Transport = token;
                        }
                        else if ( RtspHeaderProtocolValidator.IsValidTransmission( parameter.Key ) )
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
                        if ( RtspHeaderProtocolValidator.IsValidTransport( token ) )
                        {
                            header.Transport = token;
                        }
                        else if ( RtspHeaderProtocolValidator.IsValidTransmission( token ) )
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
                
                if ( RtspHeaderProtocolValidator.IsValidToken( header.ConferenceId ) )
                {
                    result = header;
                }
            }

            return result != null;
        }



        public bool AddExtension( string value )
        {
            if ( RtspHeaderProtocolValidator.IsValid( value = ValueNormalizer.Normalize( value ) ) )
            {
                return _extensions.Add( value );
            }

            return false;
        }

        public bool RemoveExtension( string value )
        {
            return _extensions.Remove( ValueNormalizer.Normalize( value ) );
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
    }
}
