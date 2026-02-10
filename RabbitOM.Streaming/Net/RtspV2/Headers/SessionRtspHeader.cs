using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class SessionRtspHeader : RtspHeader 
    {
        public const string TypeName = "Session";
        

        private string _id;

        private string _timeout;



        public string Id
        { 
            get => _id ?? string.Empty;
            set => _id = value;
        }


        public string Timeout
        {
            get => _timeout ?? string.Empty;
            set => _timeout = value;
        }




        public override bool TryValidate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }





        public static bool TryParse( string value , out SessionRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
