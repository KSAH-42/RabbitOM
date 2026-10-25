using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = false ) ]
    public sealed class PositionAttribute : Attribute
    {
        public PositionAttribute( int value )
        {
            Value = value;
        }

        public int Value { get; }
    }
}
