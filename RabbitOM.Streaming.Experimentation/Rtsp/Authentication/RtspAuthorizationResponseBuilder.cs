using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Authentication
{
    public sealed class RtspAuthorizationResponseBuilder
    {
        private string _method = string.Empty;
        private string _scheme = string.Empty;
        private string _algorithm = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _uri = string.Empty;
        private string _realm = string.Empty;
        private string _nonce = string.Empty;





        public string Method
        {
            get => _method;
            set => _method = value ?? string.Empty;
        }

        public string Scheme
        {
            get => _scheme;
            set => _scheme = value ?? string.Empty;
        }
        
        public string Algorithm
        {
            get => _algorithm;
            set => _algorithm = value ?? string.Empty;
        }
        
        public string UserName
        {
            get => _username;
            set => _username = value ?? string.Empty;
        }
        
        public string Password
        {
            get => _password;
            set => _password = value ?? string.Empty;
        }
        
        public string Uri
        {
            get => _uri;
            set => _uri = value ?? string.Empty;
        }
        
        public string Realm
        {
            get => _realm;
            set => _realm = value ?? string.Empty;
        }
        
        public string Nonce
        {
            get => _nonce;
            set => _nonce = value ?? string.Empty;
        }
        




        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( _username ) || string.IsNullOrWhiteSpace( _password ) )
            {
                return string.Empty;
            }

            if ( RtspAuthenticationSchemes.IsBasicAuthentication( _scheme ) )
            {
                return RtspAuthorizationAlgorithms.ComputeAsBasic( $"{_username}:{_password}" );
            }

            if ( RtspAuthenticationSchemes.IsDigestAuthentication( _scheme ) )
            {
                if ( string.IsNullOrWhiteSpace( _method ) || string.IsNullOrWhiteSpace( _uri ) || string.IsNullOrWhiteSpace( _realm ) || string.IsNullOrWhiteSpace( _nonce ) )
                {
                    return string.Empty;
                }

                string ComputeHash( Func<string,string> createHash )
                {
                    var hashA1 = createHash( _username + ":" + _realm + ":" + _password );
                    var hashA2 = createHash( _method + ":" + _uri  );

                    return createHash( hashA1 + ":" + _nonce + ":" + hashA2 );
                }

                if ( RtspAuthenticationSchemes.IsMd5Algorithm( _algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsMD5 );
                }

                if ( RtspAuthenticationSchemes.IsSha1Algorithm( _algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsSHA1 );
                }

                if ( RtspAuthenticationSchemes.IsSha256Algorithm( _algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsSHA256 );
                }

                if ( RtspAuthenticationSchemes.IsSha384Algorithm( _algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsSHA384 );
                }

                if ( RtspAuthenticationSchemes.IsSha512Algorithm( _algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsSHA512 );
                }
            }

            return string.Empty;
        }  
    }
}
