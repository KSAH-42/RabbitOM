using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Authentication
{
    public sealed class RtspAuthorizationResponseBuilder
    {
        public RtspMethod Method { get; set; }
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

            if ( RtspAuthenticationTypes.IsBasicAuthentication( Scheme ) )
            {
                return RtspAuthorizationAlgorithm.ComputeAsBasic( $"{UserName}:{Password}" );
            }

            if ( RtspAuthenticationTypes.IsDigestAuthentication( Scheme ) )
            {
                if ( Method == null || string.IsNullOrWhiteSpace( Uri ) || string.IsNullOrWhiteSpace( Realm ) || string.IsNullOrWhiteSpace( Nonce ) )
                {
                    return string.Empty;
                }

                string ComputeHash( Func<string,string> algorithm )
                {
                    var hashA1 = algorithm( UserName + ":" + Realm + ":" + Password );
                    var hashA2 = algorithm( Method.Name + ":" + Uri  );

                    return algorithm( hashA1 + ":" + Nonce + ":" + hashA2 );
                }

                if ( RtspAuthenticationTypes.IsMd5Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithm.ComputeAsMD5 );
                }

                if ( RtspAuthenticationTypes.IsSha1Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithm.ComputeAsSHA1 );
                }

                if ( RtspAuthenticationTypes.IsSha256Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithm.ComputeAsSHA256 );
                }

                if ( RtspAuthenticationTypes.IsSha384Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithm.ComputeAsSHA384 );
                }

                if ( RtspAuthenticationTypes.IsSha512Algorithm( Algorithm ) )
                {
                    return ComputeHash( RtspAuthorizationAlgorithm.ComputeAsSHA512 );
                }
            }

            return string.Empty;
        }  
    }
}
