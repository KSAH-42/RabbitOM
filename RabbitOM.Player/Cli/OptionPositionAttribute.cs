using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = false ) ]
    public class OptionPositionAttribute : Attribute
    {
        public OptionPositionAttribute( int value )
        {
            Value = value;
        }

        public int Value { get; }
    }
}
