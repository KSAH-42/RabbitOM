using System;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public sealed class RangeItem
    {
        public static readonly RangeItem Zero = new RangeItem( 0 , 0 );

        public RangeItem( long from , long to )
        {
            From = from;
            To = to;
        }

        public long From { get; }
        public long To { get; }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public static RangeItem Parse( string value )
        {
            throw new NotImplementedException();
        }

        public static bool TryParse( string value , out RangeItem result )
        {
            throw new NotImplementedException();
        }
    }
}
