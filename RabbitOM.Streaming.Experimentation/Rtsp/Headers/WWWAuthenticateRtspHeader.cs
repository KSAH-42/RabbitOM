using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class WWWAuthenticateRtspHeader : RtspHeader
    {
        private string _type = string.Empty;
        private string _realm = string.Empty;
        private string _nonce = string.Empty;
        private string _opaque = string.Empty;
        private string _algorithm = string.Empty;
        private string _stale = string.Empty;




        public string Type { get => _type; }
        
        public string Realm { get => _realm; }
        
        public string Nonce { get => _nonce; }
        
        public string Opaque { get => _opaque; }
        
        public string Algorithm { get => _algorithm; }
        
        public string Stale { get => _stale; }




        
        public override bool TryValidate()
        {
            return StringRtspValidator.TryValidate( _type );
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public void SetType( string value )
        {
            _type = StringRtspNormalizer.Normalize( value );
        }

        public void SetRealm( string value )
        {
            _realm = StringRtspNormalizer.Normalize( value );
        }

        public void SetNonce( string value )
        {
            _nonce = StringRtspNormalizer.Normalize( value );
        }

        public void SetOpaque( string value )
        {
            _opaque = StringRtspNormalizer.Normalize( value );
        }

        public void SetAlgorithm( string value )
        {
            _algorithm = StringRtspNormalizer.Normalize( value );
        }

        public void SetStale( string value )
        {
            _stale = StringRtspNormalizer.Normalize( value );
        }

        


        public static bool TryParse( string value , out WWWAuthenticateRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
