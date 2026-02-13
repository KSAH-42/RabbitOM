using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RangeItem
    {
        public static readonly RangeItem Zero = new RangeItem( 0 , 0 );

        public RangeItem( long from , long to )
        {
            From = from;
            To = to;
        }


        public long? From { get; }

        public long? To { get; }



        public static bool TryParse( string value , out RangeItem result )
        {
            throw new NotImplementedException();
        }


        public override string ToString()
        {
            return From.HasValue && To.HasValue ? $"{From}-{To}" : string.Empty;
        }
    }
}
