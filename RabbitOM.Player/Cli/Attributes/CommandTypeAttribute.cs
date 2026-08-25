using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Class , AllowMultiple = true ) ]
    public sealed class CommandTypeAttribute : Attribute
    {
        public CommandTypeAttribute( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            Name = name;
        }

        public string Name { get; }
    }
}
