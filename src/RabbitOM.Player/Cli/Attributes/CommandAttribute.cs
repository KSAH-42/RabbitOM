using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Class , AllowMultiple = false ) ]
    public sealed class CommandAttribute : Attribute
    {
        public CommandAttribute( string verb )
        {
            if ( string.IsNullOrWhiteSpace( verb ) )
            {
                throw new ArgumentNullException( nameof( verb ) );
            }

            Verb = verb;
        }

        public string Verb { get; }
    }
}
