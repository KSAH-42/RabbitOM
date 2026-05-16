using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Authentication
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
                return Convert.ToBase64String( Encoding.UTF8.GetBytes( $"{_username}:{_password}" ) );
            }
            
            if ( RtspAuthenticationSchemes.IsDigestAuthentication( _scheme ) )
            {
                if ( string.IsNullOrWhiteSpace( _method ) || string.IsNullOrWhiteSpace( _uri ) || string.IsNullOrWhiteSpace( _realm ) || string.IsNullOrWhiteSpace( _nonce ) )
                {
                    return string.Empty;
                }

                RtspAuthorisationHashAlgorithm hashAlgorithm = null;

                if ( RtspAuthenticationSchemes.IsMd5Algorithm( _algorithm ) )
                {
                    hashAlgorithm = RtspAuthorisationHashAlgorithm.CreateMD5();
                }

                else if ( RtspAuthenticationSchemes.IsSha1Algorithm( _algorithm ) )
                {
                    hashAlgorithm = RtspAuthorisationHashAlgorithm.CreateSHA1();
                }

                else if ( RtspAuthenticationSchemes.IsSha256Algorithm( _algorithm ) )
                {
                    hashAlgorithm = RtspAuthorisationHashAlgorithm.CreateSHA256();
                }

                else if ( RtspAuthenticationSchemes.IsSha384Algorithm( _algorithm ) )
                {
                    hashAlgorithm = RtspAuthorisationHashAlgorithm.CreateSHA384();
                }

                else if ( RtspAuthenticationSchemes.IsSha512Algorithm( _algorithm ) )
                {
                    hashAlgorithm = RtspAuthorisationHashAlgorithm.CreateSHA512();
                }
                else
                {
                    return string.Empty;
                }

                using( hashAlgorithm )
                {
                    var hashA1 = hashAlgorithm.Compute( _username + ":" + _realm + ":" + _password );
                    var hashA2 = hashAlgorithm.Compute( _method + ":" + _uri  );

                    return hashAlgorithm.Compute( hashA1 + ":" + _nonce + ":" + hashA2 );
                }
            }

            return string.Empty;
        }  
    }
}
