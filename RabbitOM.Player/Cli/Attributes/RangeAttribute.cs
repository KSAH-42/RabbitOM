using System;

namespace RabbitOM.Player.Cli
{
    [AttributeUsage( AttributeTargets.Property , AllowMultiple = true ) ]
    public sealed class RangeAttribute : Attribute
    {
        public RangeAttribute( string name , int minimum , int maximum )
        {
            Name = name;
            Minimum = minimum;
            Maximum = maximum;
        }

        public string Name { get; }

        public int Minimum { get; }

        public int Maximum { get; }

        public static int RangeValue( RangeAttribute attribute , int value )
        {
            if ( attribute == null )
            {
                throw new ArgumentNullException( nameof( attribute ) );
            }

            if ( value < attribute.Minimum )
            {
                return attribute.Minimum;
            }

            if ( value > attribute.Maximum )
            {
                return attribute.Maximum;
            }

            return value;
        }

    }
}
