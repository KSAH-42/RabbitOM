using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class UserAgentRtspHeader : RtspHeader 
    {
        public const string TypeName = "User-Agent";
        

        public string Product { get; }
        public string Version { get; }
        public string Comments { get; }


        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( Product )
                || ! string.IsNullOrWhiteSpace( Version )
                || ! string.IsNullOrWhiteSpace( Comments)
                ;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }





        public static bool TryParse( string value , out UserAgentRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
