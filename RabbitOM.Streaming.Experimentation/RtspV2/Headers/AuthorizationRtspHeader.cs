using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public sealed class AuthorizationRtspHeader : RtspHeader
    {
        public const string TypeName = "Authorization";



        public string Type { get; private set; } = string.Empty;

        public string UserName { get; private set; } = string.Empty;

        public string Realm { get; private set; } = string.Empty;

        public string Nonce { get; private set; } = string.Empty;

        public string Domain { get; private set; } = string.Empty;

        public string Opaque { get; private set; } = string.Empty;

        public string Uri { get; private set; } = string.Empty;

        public string Response { get; private set; } = string.Empty;



        
        public override bool TryValidate()
        {
            if ( RtspAuthenticationTypes.IsBasicAuthentication( Type ) )
            {
                return StringRtspValidator.TryValidate( Response );
            }

            return RtspAuthenticationTypes.IsDigestAuthentication( Type ) 
                 ? StringRtspValidator.TryValidate( UserName )
                || StringRtspValidator.TryValidate( Realm )
                || StringRtspValidator.TryValidate( Nonce )
                || StringRtspValidator.TryValidate( Response )
                || StringRtspValidator.TryValidateUri( Uri )
                 : false
                 ;
        }

        public void SetType( string value )
        {
            Type = StringRtspNormalizer.Normalize( value );
        }

        public void SetUserName( string value )
        {
            UserName = StringRtspNormalizer.Normalize( value );
        }

        public void SetRealm( string value )
        {
            Realm = StringRtspNormalizer.Normalize( value );
        }

        public void SetNonce( string value )
        {
            Nonce = StringRtspNormalizer.Normalize( value );
        }

        public void SetDomain( string value )
        {
            Domain = StringRtspNormalizer.Normalize( value );
        }

        public void SetOpaque( string value )
        {
            Opaque = StringRtspNormalizer.Normalize( value );
        }

        public void SetUri( string value )
        {
            Uri = StringRtspNormalizer.Normalize( value );
        }

        public void SetResponse( string value )
        {
            Response = StringRtspNormalizer.Normalize( value );
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        


        public static bool TryParse( string value , out AuthorizationRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
