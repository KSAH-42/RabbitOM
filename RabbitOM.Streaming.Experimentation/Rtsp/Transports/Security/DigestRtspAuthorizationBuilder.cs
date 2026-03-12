using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Security
{
    public class DigestRtspAuthorizationBuilder : RtspAuthorizationBuilder
    {
        public RtspMethod Method { get; set; }
        public string Scheme { get; set; }
        public string Uri { get; set; }
        public string Realm { get; set; }
        public string Nonce { get; set; }

        
        public override bool CanBuild()
        {
            throw new NotImplementedException();
        }

        public override string Build()
        {
            throw new NotImplementedException();
        }



        protected virtual string CreateMD5Authorization()
        {
            throw new NotImplementedException();
        }

        protected virtual string CreateSHA1Authorization()
        {
            throw new NotImplementedException();
        }

        protected virtual string CreateSHA256Authorization()
        {
            throw new NotImplementedException();
        }

        protected virtual string CreateSHA512Authorization()
        {
            throw new NotImplementedException();
        }
    }
}
