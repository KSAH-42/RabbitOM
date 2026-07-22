using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp
{
    public sealed class RepeatFieldCollection : FieldCollection<RepeatField>
    {
        public RepeatFieldCollection()
        {
        }

        public RepeatFieldCollection(IEnumerable<RepeatField> fields) : base(fields)
        {
        }
    }
}
