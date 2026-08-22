using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = true ) ]
    public class OptionAttribute : Attribute
    {
        public OptionAttribute( string name )
        {
            Name = name;
        }

        public OptionAttribute( string name , object defaultValue )
        {
            Name = name;
            DefaultValue = defaultValue;
        }

        public string Name { get; }

        public object DefaultValue { get; }
    }
}
