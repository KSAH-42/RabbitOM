using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = false ) ]
    public class PositionAttribute : Attribute
    {
        public PositionAttribute( int value )
        {
            Value = value;
        }

        public int Value { get; }
    }
}
