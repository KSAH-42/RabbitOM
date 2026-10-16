using System;

namespace RabbitOM.Node.Application
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true)]
    public sealed class OptionAttribute : Attribute
    {
        public bool IsRequired { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public string Help { get; set; }
    }
}