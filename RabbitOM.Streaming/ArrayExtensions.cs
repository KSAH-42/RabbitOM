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
        public static bool IsEqualTo( this byte[] source , byte[] target )
        {
            if ( source == target )
            {
                return true;
            }

            if ( source == null || target == null || source.Length != target.Length )
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
    }
}
