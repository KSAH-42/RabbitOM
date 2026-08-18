using System;

namespace RabbitOM.Player.Controls
{
    public static class NetworkStatisticsHelper
    {
        public static void IncrementValue( ref long memberValue , long value , ref long ticks )
        {
            if ( value < 0 )
            {
                throw new ArgumentException( nameof( value ) );
            }

            var sum = memberValue + value;

            memberValue = sum > 0 ? sum : long.MaxValue;

            ticks = DateTime.Now.Ticks;
        }

        public static long GetAverageValue( ref long memberValue , ref long ticks )
        {
            var totalSeconds = (long) TimeSpan.FromTicks( DateTime.Now.Ticks - ticks ).TotalSeconds;

            var result = totalSeconds > 0 ? memberValue / totalSeconds : memberValue ;

            memberValue = 0;

            return result;
        }

        public static long GetAverageValue( ref long memberValue , ref long maxMemberValue , ref long ticks )
        {
            var totalSeconds = (long) TimeSpan.FromTicks( DateTime.Now.Ticks - ticks ).TotalSeconds;

            var result = totalSeconds > 0 ? memberValue / totalSeconds : memberValue ;

            if ( maxMemberValue < result )
            {
                maxMemberValue = result;
            }

            memberValue = 0;

            return maxMemberValue;
        }
    }
}
