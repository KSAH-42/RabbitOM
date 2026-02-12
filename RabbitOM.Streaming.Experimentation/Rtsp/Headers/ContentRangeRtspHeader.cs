using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentRangeRtspHeader : RtspHeader 
    {
        public const string TypeName = "Content-Range";
        




        private string _unit = string.Empty;
        private long? _from;
        private long? _to;
        private long? _size;
        



        public string Unit
        {
            get => _unit;
            set => _unit = StringRtspNormalizer.Normalize( value );
        }

        public long? From
        {
            get => _from;
            set => _from = value;
        }

        public long? To
        {
            get => _to;
            set => _to = value;
        }

        public long? Size
        {
            get => _size;
            set => _size = value;
        }





        public static bool TryParse( string input , out ContentRangeRtspHeader result )
        {
            // Content-Range: <unit> <range>/<size>
            // Content-Range: <unit> <range>/*
            // Content-Range: <unit> */size
         
            throw new NotImplementedException();
        }
        




        public override bool TryValidate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
