using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Authentication
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class RtspAuthorizationResponseBuilder
    {
        public string Method { get; set; }

        public string Scheme { get; set; }
        
        public string Algorithm { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string Uri { get; set; }
        
        public string Realm { get; set; }
        
        public string Nonce { get; set; }






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
