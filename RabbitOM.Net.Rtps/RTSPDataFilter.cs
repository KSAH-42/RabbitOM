using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent an helper class used to filter or data input data
    /// </summary>
    public static class RTSPDataFilter
    {
        /// <summary>
        /// Convert a value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>returns a value</returns>
        public static string Trim( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
            }

            return value.Trim();
        }

        /// <summary>
        /// Adapt the value in case where the value is outside the specified range
        /// </summary>
        /// <typeparam name="TValue">the type of the value</typeparam>
        /// <param name="value">the value</param>
        /// <param name="minimum">the minimum range</param>
        /// <param name="maximum">the maximum range</param>
        /// <returns>returns a valye</returns>
        /// <exception cref="ArgumentException"/>
        public static TValue Adapt<TValue>( TValue value , TValue minimum , TValue maximum )
            where TValue : struct, IComparable<TValue>
        {
            if ( minimum.CompareTo( maximum ) >= 0 )
            {
                throw new ArgumentException( nameof( minimum ) );
            }

            if ( value.CompareTo( minimum ) < 0 )
            {
                return minimum;
            }

            if ( value.CompareTo( maximum ) > 0 )
            {
                return maximum;
            }

            return value;
        }
    }
}
