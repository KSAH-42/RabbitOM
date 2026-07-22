using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp
{
    public sealed class AttributeFieldCollection : FieldCollection<AttributeField>
    {
        public AttributeFieldCollection()
        {
        }

        public AttributeFieldCollection(IEnumerable<AttributeField> fields) : base( fields )
        {
        }
    }
}
