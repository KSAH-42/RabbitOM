using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    /// <summary>
    /// Represent the source description item
    /// </summary>
    public sealed class SourceDescriptionItem
    {
        /// <summary>
        /// The constant size
        /// </summary>
        public const int MinimumSize = 2;






        /// <summary>
        /// Initialize a new instance
        /// </summary>
        private SourceDescriptionItem()
        {
        }

        /// <summary>
        /// /// Initialize a new instance
        /// </summary>
        /// <param name="type">the type</param>
        /// <param name="value">the value</param>
        public SourceDescriptionItem( byte type , string value )
        {
            Type = type;
            Value = value ?? string.Empty;
        }
        





        /// <summary>
        /// Gets the type
        /// </summary>
        public byte Type { get; private set; }
        
        /// <summary>
        /// Gets the value
        /// </summary>
        public string Value { get; private set; }
        





        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="payload">the payload</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( in ArraySegment<byte> payload , out SourceDescriptionItem result )
        {
            result = null;

            if ( payload.Count < MinimumSize )
            {
                return false;
            }

            var offset = payload.Array[ payload.Offset ];

            result = new SourceDescriptionItem();

            result.Type = payload.Array[ offset ++ ];

            var textBuffer = new byte[ payload.Array[ offset ++ ] ];

            if ( textBuffer.Length > 0 )
            {
                for ( var i = 0 ; i < textBuffer.Length && offset < payload.Array.Length ; ++ i )
                {
                    textBuffer[ i ] = payload.Array[ ++ offset ];
                }

                result.Value = System.Text.Encoding.ASCII.GetString( textBuffer ) ?? string.Empty;
            }

            return true;
        }
    }
}
