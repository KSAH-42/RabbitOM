using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class CSeqRtspHeader : RtspHeader 
    {
        public const string TypeName = "CSeq";
        


        private int _id;



        public int Id
        {
            get => _id;
            set => _id = value;
        }


        public override bool TryValidate()
        {
            return _id >= 0;
        }

        public override string ToString()
        {
            return _id.ToString();
        }
        




        public static bool TryParse( string value , out CSeqRtspHeader result )
        {
            result = null;

            if ( ! int.TryParse( value , out var number ) )
            {
                return false;
            }

            result = new CSeqRtspHeader() { Id = number };

            return true;
        }
    }
}
