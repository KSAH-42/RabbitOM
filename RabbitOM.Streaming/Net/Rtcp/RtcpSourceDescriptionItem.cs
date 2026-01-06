using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public sealed class RtcpSourceDescriptionItem
    {
        public const int MinimumSize = 2;



        public byte Type { get; private set; }
        
        public string Value { get; private set; }




        public static bool TryParse( in ArraySegment<byte> payload , out RtcpSourceDescriptionItem result )
        {
            result = null;

            if ( payload.Count < MinimumSize )
            {
                return false;
            }

            var offset = payload.Array[ payload.Offset ];

            result = new RtcpSourceDescriptionItem();

            result.Type = payload.Array[ offset ++ ];

            var textBuffer = new byte[ payload.Array[ offset ++ ] ];

            if ( textBuffer.Length > 0 )
            {
                for ( var i = 0 ; i < textBuffer.Length && offset < payload.Array.Length ; ++ i )
                {
                    textBuffer[ i ] = payload.Array[ ++ offset ];
                }

                result.Value = System.Text.Encoding.ASCII.GetString( textBuffer );
            }

            return true;
        }
    }
}
