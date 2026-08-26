using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Class ) ]
    public class HelpAttribute : Attribute
    {
        public HelpAttribute( string value )
        {
            ResourceKey = value;
        }

        public string ResourceKey { get; }
    }
}
