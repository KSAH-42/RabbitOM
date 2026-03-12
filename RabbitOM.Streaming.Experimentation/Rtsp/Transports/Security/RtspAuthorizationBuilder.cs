using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Security
{
    public sealed class RtspAuthorizationBuilder
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

                if ( RtspAuthenticationTypes.IsMd5Algorithm( Algorithm ) )
                {
                    var hashA1 = RtspAuthorizationAlgorithm.ComputeAsMD5( UserName + ":" + Realm + ":" + Password );
                    var hashA2 = RtspAuthorizationAlgorithm.ComputeAsMD5( Method.Name + ":" + Uri  );

                    return RtspAuthorizationAlgorithm.ComputeAsMD5( hashA1 + ":" + Nonce + ":" + hashA2 );
                }

                if ( RtspAuthenticationTypes.IsSha1Algorithm( Algorithm ) )
                {
                    var hashA1 = RtspAuthorizationAlgorithm.ComputeAsSHA1( UserName + ":" + Realm + ":" + Password );
                    var hashA2 = RtspAuthorizationAlgorithm.ComputeAsSHA1( Method.Name + ":" + Uri  );

                    return RtspAuthorizationAlgorithm.ComputeAsSHA1( hashA1 + ":" + Nonce + ":" + hashA2 );
                }

                if ( RtspAuthenticationTypes.IsSha256Algorithm( Algorithm ) )
                {
                    var hashA1 = RtspAuthorizationAlgorithm.ComputeAsSHA256( UserName + ":" + Realm + ":" + Password );
                    var hashA2 = RtspAuthorizationAlgorithm.ComputeAsSHA256( Method.Name + ":" + Uri  );

                    return RtspAuthorizationAlgorithm.ComputeAsSHA256( hashA1 + ":" + Nonce + ":" + hashA2 );
                }

                if ( RtspAuthenticationTypes.IsSha512Algorithm( Algorithm ) )
                {
                    var hashA1 = RtspAuthorizationAlgorithm.ComputeAsSHA512( UserName + ":" + Realm + ":" + Password );
                    var hashA2 = RtspAuthorizationAlgorithm.ComputeAsSHA512( Method.Name + ":" + Uri  );

                    return RtspAuthorizationAlgorithm.ComputeAsSHA512( hashA1 + ":" + Nonce + ":" + hashA2 );
                }
            }

            return string.Empty;
        }  
    }
}
