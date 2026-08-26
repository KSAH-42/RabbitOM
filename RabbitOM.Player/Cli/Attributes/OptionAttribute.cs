using System;

namespace RabbitOM.Player.Cli
{
    public abstract class OptionAttribute : Attribute
    {
        protected OptionAttribute( string name )
        {
            Name = name;
        }

        public string Name { get; }
    }
}
