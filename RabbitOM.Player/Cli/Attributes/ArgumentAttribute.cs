using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = true ) ]
    public sealed class ArgumentAttribute : Attribute
    {
        public ArgumentAttribute( string name )
        {
            Name = name;
        }

        public string Name { get; }
    }
}
