using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp
{
    public sealed class PhoneFieldCollection : FieldCollection<PhoneField>
    {
        public PhoneFieldCollection()
        {
        }

        public PhoneFieldCollection(IEnumerable<PhoneField> fields) : base(fields)
        {
        }
    }
}
