using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property ) ]
    public sealed class RequiredAttribute : Attribute
    {
    }
}
