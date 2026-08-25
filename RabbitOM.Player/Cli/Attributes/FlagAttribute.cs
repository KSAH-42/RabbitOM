using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = true ) ]
    public class FlagAttribute : OptionAttribute
    {
        public FlagAttribute( string name , bool value )
            : base( name )
        {
            Value = value;
        }

        public bool Value { get; }
    }
}
