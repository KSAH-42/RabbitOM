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
        public static bool IsEquals( this byte[] source , byte[] target )
        {
            if ( object.ReferenceEquals( source , target ) )
            {
                return true;
            }

            if ( object.ReferenceEquals( source , null ) || object.ReferenceEquals( target , null ) )
            {
                return false;
            }

            if ( source.Length != target.Length )
            {
                return false;
            }

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
        public static bool IsEquals( this ArraySegment<byte> source , ArraySegment<byte> target )
        {
            if ( object.ReferenceEquals( source.Array , target.Array ) )
            {
                return true;
            }

            if ( object.ReferenceEquals( source.Array , null ) || object.ReferenceEquals( target.Array , null ) )
            {
                return false;
            }

            if ( source.Count != target.Count )
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
    }
}
