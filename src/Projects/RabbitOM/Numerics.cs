using System;

namespace RabbitOM
{
    public static class Numerics
    {
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
