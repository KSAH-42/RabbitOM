using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Authentication
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;

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

            if ( AuthenticationTypes.IsBasicAuthentication( _scheme ) )
            {
                return Convert.ToBase64String( Encoding.UTF8.GetBytes( $"{_username}:{_password}" ) );
            }

            if ( AuthenticationTypes.IsDigestAuthentication( _scheme ) )
            {
                if ( string.IsNullOrWhiteSpace( _method ) || string.IsNullOrWhiteSpace( _uri ) || string.IsNullOrWhiteSpace( _realm ) || string.IsNullOrWhiteSpace( _nonce ) )
                {
                    return string.Empty;
                }

                string BuildDigestResponse( RtspHashAlgorithm algorithm )
                {
                    using ( algorithm )
                    {
                        var hash1 = algorithm.Compute( _username + ":" + _realm + ":" + _password );
                        var hash2 = algorithm.Compute( _method + ":" + _uri  );

                        return algorithm.Compute( hash1 + ":" + _nonce + ":" + hash2 );
                    }
                }

                if ( AuthenticationTypes.IsMd5Algorithm( _algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateMD5() );
                }

                if ( AuthenticationTypes.IsSha1Algorithm( _algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateSHA1() );
                }

                if ( AuthenticationTypes.IsSha256Algorithm( _algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateSHA256() );
                }

                if ( AuthenticationTypes.IsSha384Algorithm( _algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateSHA384() );
                }

                if ( AuthenticationTypes.IsSha512Algorithm( _algorithm ) )
                {
                    return BuildDigestResponse( RtspHashAlgorithm.CreateSHA512() );
                }
            }

            return string.Empty;
        }
    }
}
