using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Number helper class
    /// </summary>
    public static class Numerics
    {
        /// <summary>
        /// Gets the value inside the boundary and limit the value if it comes outside the boundaries
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="min">the min</param>
        /// <param name="max">the max</param>
        /// <returns>returns a value</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static long Clamp( in long value , in long min , in long max )
        {
            if ( min > max )
            {
                throw new ArgumentException( nameof( min ) );
            }

            return value < min ? min : value > max ? max : value;
        }
    }
}
