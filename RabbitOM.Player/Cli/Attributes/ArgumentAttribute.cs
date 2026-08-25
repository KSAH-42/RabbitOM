using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = true ) ]
    public sealed class ArgumentAttribute : OptionAttribute
    {
        public ArgumentAttribute( string name )
            : base( name )
        {
        }
    }
}
