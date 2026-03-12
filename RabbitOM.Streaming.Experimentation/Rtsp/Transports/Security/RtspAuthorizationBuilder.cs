using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Security
{
    public abstract class RtspAuthorizationBuilder
    {
        public string UserName { get; set; }
        public string Password { get; set; }


        public abstract bool CanBuild();
        public abstract string Build();
    }
}
