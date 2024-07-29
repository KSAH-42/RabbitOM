using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent the jpeg fragment
    /// </summary>
    public sealed class JpegFragment
    {
        /// <summary>
        /// The minimum length
        /// </summary>
        public const int MinimumLength = 16;




        /// <summary>
        /// Gets / Sets the offset
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets / Sets the type
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets / Sets the width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets / Sets the height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets / Sets the DRI (Define Restart Interval)
        /// </summary>
        public int Dri { get; set; }

        /// <summary>
        /// Gets / Sets the MBZ (Must Be Zero)
        /// </summary>
        public int Mbz { get; set; }

        /// <summary>
        /// Gets / Sets the quantization factor
        /// </summary>
        public int QFactor { get; set; }

        /// <summary>
        /// Gets / Sets the quantization table
        /// </summary>
        public ArraySegment<byte> QTable { get; set; }

        /// <summary>
        /// Gets / Sets the data
        /// </summary>
        public ArraySegment<byte> Data { get; set; }




        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryValidate()
        {
            return Type >= 0 && Width >= 2 && Height >= 2 && Data.Count > 0;
        }




        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( ArraySegment<byte> buffer , out JpegFragment result )
        {
            result = null;

            if ( buffer.Array == null || buffer.Count < MinimumLength )
            {
                return false;
            }

            int offset = buffer.Offset + 1;
           
            result = new JpegFragment()
            {
                Offset  = buffer.Array[ offset++ ]  << 16 | buffer.Array[ offset++] << 8 | buffer.Array[ offset ++ ] ,
                Type    = buffer.Array[ offset++ ] ,
                QFactor = buffer.Array[ offset++ ] ,
                Width   = buffer.Array[ offset++ ] * 8 ,
                Height  = buffer.Array[ offset++ ] * 8 ,
            };

            if ( result.Type >= 64 )
            {
                result.Dri = buffer.Array[ offset++ ] << 8 | buffer.Array[ offset++ ];

                offset += 2;
            }

            if ( result.Offset == 0 && result.QFactor >= 128 )
            {
                result.Mbz = buffer.Array[ offset++ ];

                if ( result.Mbz == 0 )
                {
                    offset ++;

                    int length = buffer.Array[ offset++ ] << 8 | buffer.Array[ offset++ ];

                    if ( length >= ( buffer.Array.Length - offset ) )
                    {
                        return false;
                    }

                    result.QTable = new ArraySegment<byte>( buffer.Array , offset , length );

                    offset += length;
                }
            }

            if ( offset >= buffer.Count )
            {
                return false;
            }

            result.Data = new ArraySegment<byte>( buffer.Array , offset , buffer.Count - (offset - buffer.Offset) );
            
            return true;
        }
    }
}
