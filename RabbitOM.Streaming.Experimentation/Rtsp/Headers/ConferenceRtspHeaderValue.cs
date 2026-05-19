using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;
   
    public sealed class ConferenceRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        

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
        private readonly StringRtspHeaderValueCollection _extensions = new StringRtspHeaderValueCollection();




        public string ConferenceId
        {
            get => _conferenceId;
            set => _conferenceId = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Transport
        {
            get => _transport;
            set => _transport = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Transmission
        {
            get => _transmission;
            set => _transmission = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Source
        {
            get => _source;
            set => _source = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Destination
        {
            get => _destination;
            set => _destination = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Address
        {
            get => _address;
            set => _address = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Host
        {
            get => _host;
            set => _host = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Role
        {
            get => _role;
            set => _role = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Mode
        {
            get => _mode;
            set => _mode = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Tag
        {
            get => _tag;
            set => _tag = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Session
        {
            get => _session;
            set => _session = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string Access
        {
            get => _access;
            set => _access = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
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
        
        public StringRtspHeaderValueCollection Extensions
        {
            get => _extensions;
        }


        


        public static bool TryParse( string input , out ConferenceRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var header = new ConferenceRtspHeaderValue();

                foreach ( var token in tokens.Where( RtspHeaderValueValidator.TryEnsureWellFormedToken ) )
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
                        else if ( ValueComparer.Equals( "role" , parameter.Key ) )
                        {
                            header._role = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "mode" , parameter.Key ) )
                        {
                            header._mode = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "tag" , parameter.Key ) )
                        {
                            header._tag = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "session" , parameter.Key ) )
                        {
                            header._session = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "access" , parameter.Key ) )
                        {
                            header._access = RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value );
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
                        else if ( SupportedTypes.IsTransportSupported( parameter.Key ) )
                        {
                            header._transport = RtspHeaderValueSanitizer.UnQuotesWithTrim( token );
                        }
                        else if ( SupportedTypes.IsTransmissionSupported( parameter.Key ) )
                        {
                            header._transmission = RtspHeaderValueSanitizer.UnQuotesWithTrim( token );
                        }
                        else
                        {
                            header._extensions.TryAdd( RtspHeaderValueSanitizer.UnQuotesWithTrim( token ) );
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
                        else    
                        {
                            if ( RtspHeaderValueValidator.TryEnsureWellFormedToken( header._conferenceId ) )
                            {
                                header._extensions.TryAdd( RtspHeaderValueSanitizer.UnQuotesWithTrim( token ) );
                            }
                            else
                            {
                                header._conferenceId = RtspHeaderValueSanitizer.UnQuotesWithTrim( token );
                            }
                        }
                    }
                }
                
                if ( RtspHeaderValueValidator.TryEnsureWellFormedToken( header._conferenceId ) )
                {
                    result = header;
                }
            }

            return result != null;
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
                builder.AppendFormat( "access={0}; " , Access);
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
