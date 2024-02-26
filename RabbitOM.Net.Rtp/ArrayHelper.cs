using System;

namespace RabbitOM.Net.Rtp
{
    /// <summary>
    /// Represent the array helper
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Check equality of a bytes sequences
        /// </summary>
        /// <param name="first">the array</param>
        /// <param name="firstOffset">the offset</param>
        /// <param name="firstCount">the count</param>
        /// <param name="second">the array</param>
        /// <param name="secondOffset">the offset</param>
        /// <param name="secondCount">the count</param>
        /// <returns></returns>
        public static bool Equals( byte[] first , int firstOffset , int firstCount , byte[] second , int secondOffset , int secondCount )
        {
            if ( object.ReferenceEquals( first , second ) )
            {
                return true;
            }

            if ( first == null || second == null )
            {
                return false;
            }

            if ( firstCount != secondCount )
            {
                return false;
            }

            for ( int i = 0 ; i < firstCount ; i++ )
            {
                if ( first[ firstOffset + i ] != second[ secondOffset + i ] )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check the array start with values
        /// </summary>
        /// <param name="array">the array</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <param name="values">the values</param>
        /// <returns>returns true for a sucess, otherwise false</returns>
        public static bool StartsWith( byte[] array , int offset , int count , byte[] values )
        {        
            if ( array == null || values == null )
            {
                return false;
            }

            if ( count < values.Length )
            {
                return false;
            }

            for ( int i = 0 ; i < values.Length ; i++, offset++ )
            {
                if ( array[ offset ] != values[ i ] )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check the array end with values
        /// </summary>
        /// <param name="array">the array</param>
        /// <param name="offset">the offset</param>
        /// <param name="count">the count</param>
        /// <param name="values">the values</param>
        /// <returns>returns true for a sucess, otherwise false</returns>
        public static bool EndsWith( byte[] array , int offset , int count , byte[] values )
        {
            if ( array == null || values == null )
            {
                return false;
            }

            if ( count < values.Length )
            {
                return false;
            }

            offset = offset + count - values.Length;

            for ( int i = 0 ; i < values.Length ; i++, offset++ )
            {
                if ( array[ offset ] != values[ i ] )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Find the index of a sequence
        /// </summary>
        /// <param name="array">the array</param>
        /// <param name="sequence">the sequence</param>
        /// <param name="startIndex">the start index</param>
        /// <param name="count">the count</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static int IndexOfBytes( byte[] array , byte[] sequence , int startIndex , int count )
        {
            if ( array == null || sequence == null )
            {
                return -1;
            }

            if ( count < sequence.Length )
            {
                return -1;
            }

            int foundIndex = 0;

            for ( int endIndex = startIndex + count ; startIndex < endIndex ; startIndex++ )
            {
                if ( array[ startIndex ] != sequence[ foundIndex ] )
                {
                    foundIndex = 0;
                }
                else if ( ++foundIndex == sequence.Length )
                {
                    return startIndex - foundIndex + 1;
                }
            }

            return -1;
        }

        /// <summary>
        /// Create an array from a array segment
        /// </summary>
        /// <param name="arraySegment">the array segment</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryCreateArray( ArraySegment<byte> arraySegment , out byte[] result )
        {
            result = default;

            if ( arraySegment.Array == null || arraySegment.Array.Length <= 0 )
            {
                return false;
            }

            if ( arraySegment.Offset <= 0 )
            {
                return false;
            }

            result = new byte[ arraySegment.Array.Length - arraySegment.Offset ];

            Buffer.BlockCopy( arraySegment.Array , arraySegment.Offset , result , 0 , result.Length );

            return true;
        }
    }
}
