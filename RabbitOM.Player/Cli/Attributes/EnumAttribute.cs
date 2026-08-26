using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = true ) ]
    public sealed class EnumAttribute : OptionAttribute
    {
        public EnumAttribute( string name , int value )
            : base( name )
        {
            Value = value;
        }

        public int Value { get; }
    }
}
