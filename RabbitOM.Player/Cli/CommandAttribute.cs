using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Class ) ]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute( string verb )
        {
            Verb = verb;
        }

        public string Verb { get; }
    }
}
