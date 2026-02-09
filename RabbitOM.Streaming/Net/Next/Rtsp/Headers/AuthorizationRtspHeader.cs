using System;

namespace RabbitOM.Streaming.Net.Next.Rtsp.Headers
{
    public class AuthorizationRtspHeader : RtspHeader
    {
        private string _type = string.Empty;
        private string _userName = string.Empty;
        private string _realm = string.Empty;
        private string _nonce = string.Empty;
        private string _domain = string.Empty;
        private string _opaque = string.Empty;
        private string _uri = string.Empty;
        private string _response  = string.Empty;




        public string Type { get => _type; }

        public string UserName { get => _userName; }

        public string Realm { get => _realm; }

        public string Nonce { get => _nonce; }

        public string Domain { get => _domain; }

        public string Opaque { get => _opaque; }

        public string Uri { get => _uri; }

        public string Response { get => _response; }




        
        public override bool TryValidate()
        {
            if ( RtspAuthentication.IsBasicAuthentication( _type ) )
            {
                return StringRtspValidator.Validate( _response );
            }

            if ( RtspAuthentication.IsDigestAuthentication( _type ) )
            {
                return StringRtspValidator.Validate( _userName )
                    || StringRtspValidator.Validate( _realm )
                    || StringRtspValidator.Validate( _nonce )
                    || StringRtspValidator.Validate( _response )
                    || StringRtspValidator.ValidateUri( _uri )
                    ;
            }

            return false;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public void SetType( string value )
        {
            _type = StringRtspNormalizer.Normalize( value );
        }

        public void SetUserName( string value )
        {
            _userName = StringRtspNormalizer.Normalize( value );
        }

        public void SetRealm( string value )
        {
            _realm = StringRtspNormalizer.Normalize( value );
        }

        public void SetNonce( string value )
        {
            _nonce = StringRtspNormalizer.Normalize( value );
        }

        public void SetDomain( string value )
        {
            _domain = StringRtspNormalizer.Normalize( value );
        }

        public void SetOpaque( string value )
        {
            _opaque = StringRtspNormalizer.Normalize( value );
        }

        public void SetUri( string value )
        {
            _uri = StringRtspNormalizer.Normalize( value );
        }

        public void SetResponse( string value )
        {
            _response = StringRtspNormalizer.Normalize( value );
        }




        public static bool TryParse( string value , out AuthorizationRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
