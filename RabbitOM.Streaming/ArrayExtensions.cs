using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent an array extensions class
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Byte array comparison
        /// </summary>
        /// <param name="source">the source</param>
        /// <param name="target">the target</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool SequenceEquals( this byte[] source , byte[] target )
        {
            if ( source == target )
            {
                return true;
            }

            if ( source == null || target == null || source.Length != target.Length )
            {
                return false;
            }
            
            // use this, linq is too slow
            for ( int i = 0 ; i < source.Length ; ++ i )
            {
                if ( source[ i ] != target[ i ] )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Array segment comparison
        /// </summary>
        /// <param name="source">the source</param>
        /// <param name="target">the target</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool SequenceEquals( this ArraySegment<byte> source , ArraySegment<byte> target )
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
    }
}
