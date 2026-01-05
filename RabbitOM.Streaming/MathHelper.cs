using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Math help class
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Gets the value inside the boundary
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
