using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the collection for attribute field that a allow duplicate field names
    /// </summary>
    public sealed class AttributeFieldCollection : FieldCollection<AttributeField>
    {
        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        public AttributeFieldCollection()
        {
        }

        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        /// <param name="fields">the collection of field</param>
        public AttributeFieldCollection(IEnumerable<AttributeField> fields)
            : base( fields )
        {
        }
    }
}
