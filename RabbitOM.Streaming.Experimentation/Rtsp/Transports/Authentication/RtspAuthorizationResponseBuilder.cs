using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Authentication
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

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
            if ( string.IsNullOrWhiteSpace( UserName ) || string.IsNullOrWhiteSpace( Password ) )
            {
                return string.Empty;
            }

            if ( AuthenticationTypes.IsBasicAuthentication( Scheme ) )
            {
                return RtspAuthorizationAlgorithms.ComputeAsBasic( $"{UserName}:{Password}" );
            }

            if ( AuthenticationTypes.IsDigestAuthentication( Scheme ) )
            {
                if ( string.IsNullOrWhiteSpace( Method ) || string.IsNullOrWhiteSpace( Uri ) || string.IsNullOrWhiteSpace( Realm ) || string.IsNullOrWhiteSpace( Nonce ) )
                {
                    return string.Empty;
                }

                string ComputeHash( Func<string,string> hashFunction )
                {
                    var hashA1 = hashFunction( UserName + ":" + Realm + ":" + Password );
                    var hashA2 = hashFunction( Method + ":" + Uri  );

                    return hashFunction( hashA1 + ":" + Nonce + ":" + hashA2 );
                }

                if ( AuthenticationTypes.IsMd5Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsMD5 );
                }

                if ( AuthenticationTypes.IsSha1Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsSHA1 );
                }

                if ( AuthenticationTypes.IsSha256Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsSHA256 );
                }

                if ( AuthenticationTypes.IsSha384Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsSHA384 );
                }

                if ( AuthenticationTypes.IsSha512Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithms.ComputeAsSHA512 );
                }
            }

            return string.Empty;
        }  
    }
}
