using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = true ) ]
    public class CommandOptionAttribute : Attribute
    {
        public CommandOptionAttribute( string name )
        {
            Name = name;
        }

        public string Name { get; }
    }
}
