using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Class , AllowMultiple = false ) ]
    public sealed class HelpAttribute : Attribute
    {
        public HelpAttribute( string value )
        {
            ResourceKey = value;
        }

        public string ResourceKey { get; }
    }
}
