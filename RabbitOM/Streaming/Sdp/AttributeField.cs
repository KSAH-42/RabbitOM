using System;

namespace RabbitOM.Streaming.Sdp
{
    using RabbitOM.Streaming.Sdp.Serialization.Formatters;

    public sealed class AttributeField : BaseField , ICopyable<AttributeField>
    {
        public const string TypeNameValue = "a";






        private string _name  = string.Empty;

        private string _value = string.Empty;







        public AttributeField()
        {
        }

        public AttributeField(string name) : this(name, string.Empty)
        {
        }

        public AttributeField(string name, string value)
        {
            Name  = name;
            Value = value;
        }







        public override string TypeName
        {
            get => TypeNameValue;
        }

        public string Name
        {
            get => _name;
            set => _name = DataConverter.Filter(value);
        }

        public string Value
        {
            get => _value;
            set => _value = DataConverter.Filter(value);
        }







        public static bool NameEquals(AttributeField field, string name )
        {
            return NameEquals( field , name , true );
        }

        public static bool NameEquals(AttributeField field , string name , bool ignoreCase )
        {
            if ( field == null )
            {
                return false;
            }

            return string.Compare( field.Name ?? string.Empty , name ?? string.Empty , ignoreCase ) == 0 ;
        }







        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _name );
        }

        public void CopyFrom(AttributeField field)
        {
            if ( field == null )
            {
                return;
            }

            _name  = field._name;
            _value = field._value;
        }

        public override string ToString()
        {
            return AttributeFieldFormatter.Format(this);
        }





        public static AttributeField Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }

            return AttributeFieldFormatter.TryParse(value, out AttributeField result) ? result : throw new FormatException();
        }

        public static bool TryParse(string value, out AttributeField result)
        {
            return AttributeFieldFormatter.TryParse(value, out result);
        }
    }
}
