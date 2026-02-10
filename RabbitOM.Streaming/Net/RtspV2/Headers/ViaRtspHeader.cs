using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class ViaRtspHeader : RtspHeader 
    { 
        public const string TypeName = "Via";
        

        public string Comment { get; set; }
        public string ProtocolName { get; set; }
        public string ProtocolVersion { get; set; }
        public string ReceivedBy { get; set; }


        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( Comment )
                || ! string.IsNullOrWhiteSpace( ProtocolName )
                || ! string.IsNullOrWhiteSpace( ProtocolVersion )
                || ! string.IsNullOrWhiteSpace( ReceivedBy )
                ;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }





        public static ViaRtspHeader Parse( string value )
        {
            throw new NotImplementedException();
        }

        public static bool TryParse( string value , out ViaRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
