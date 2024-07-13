using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent an array extensions class
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Byte array comparision
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
    }
}
