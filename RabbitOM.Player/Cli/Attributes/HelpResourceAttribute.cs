using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Class , AllowMultiple = false ) ]
    public sealed class HelpResourceAttribute : Attribute
    {
        public HelpResourceAttribute( string key )
        {
            Key = key;
        }

        public string Key { get; }
    }
}
