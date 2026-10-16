using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = false ) ]
    public sealed class RequiredAttribute : Attribute
    {
    }
}
