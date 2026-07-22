using System;
using System.Linq;

namespace RabbitOM.Streaming.Sdp
{
    public static class AttributeFieldCollectionExtensions
    {
        public static AttributeField GetByName( this AttributeFieldCollection collection , string name)
        {
            return GetByName( collection , name , true );
        }

        public static AttributeField GetByName( this AttributeFieldCollection collection , string name , bool ignoreCase )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( nameof( collection ) );
            }

            var result = collection.First( field => AttributeField.NameEquals( field , name , ignoreCase ) );

            if ( result == null )
            {
                throw new InvalidOperationException("no field found");
            }

            return result;
        }

        public static AttributeField FindByName( this AttributeFieldCollection collection , string name )
        {
            return FindByName( collection , name , true );
        }

        public static AttributeField FindByName( this AttributeFieldCollection collection , string name , bool ignoreCase )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.FirstOrDefault( field => AttributeField.NameEquals( field , name , ignoreCase ) );
        }

        public static bool TryGetByName( this AttributeFieldCollection collection , string name , out AttributeField result )
        {
            return TryGetByName( collection , name, true, out result);
        }

        public static bool TryGetByName( this AttributeFieldCollection collection , string name, bool ignoreCase , out AttributeField result )
        {
            result = collection?.FirstOrDefault( field => AttributeField.NameEquals( field , name , ignoreCase ) );

            return result != null;
        }
    }
}
