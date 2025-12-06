using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent an array segment extensions class
    /// </summary>
    public static class ArraySegmentExtensions
    {
        private static readonly byte[] s_empty_buffer = new byte[0];



        /// <summary>
        /// Array segment comparison
        /// </summary>
        /// <param name="source">the source</param>
        /// <param name="target">the target</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsEqualTo( this ArraySegment<byte> source , ArraySegment<byte> target )
        {
            if ( object.ReferenceEquals( source.Array , target.Array ) )
            {
                return true;
            }

            if ( source.Array == null || target.Array == null || source.Count != target.Count )
            {
                return false;
            }

            for ( int i = 0 ; i < source.Count ; ++i )
            {
                if ( source.Array[ i + source.Offset ] != target.Array[ i + target.Offset ] )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if the byte array
        /// </summary>
        /// <param name="source">the source</param>
        /// <param name="target">the target</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool StartsWith( this ArraySegment<byte> source , byte[] target )
        {
            if ( source.Array == null || source.Count < target.Length )
            {
                return false;
            }

            for ( int i = 0 ; i < target.Length ; ++ i )
            {
                if ( target[ i ] != source.Array[ i + source.Offset ] )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Create a bytes array from an array segment
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>returns an array</returns>
        public static byte[] ToArray( this ArraySegment<byte> source )
        {
            if ( source.Count <= 0 )
            {
                return s_empty_buffer;
            }

            var result = new byte[ source.Count ];

            Buffer.BlockCopy( source.Array , source.Offset , result , 0 , result.Length );

            return result;
        }
    }
}
