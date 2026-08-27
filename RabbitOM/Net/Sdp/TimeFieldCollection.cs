using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Sdp
{
    public sealed class TimeFieldCollection : FieldCollection<TimeField>
    {
        public TimeFieldCollection()
        {
        }

        public TimeFieldCollection(IEnumerable<TimeField> fields) : base(fields)
        {
        }
    }
}
