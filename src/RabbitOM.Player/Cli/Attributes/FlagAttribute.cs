using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = true ) ]
    public sealed class FlagAttribute : Attribute
    {
        public FlagAttribute( string name , bool value )
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public bool Value { get; }
    }
}
