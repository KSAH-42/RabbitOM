using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Sdp
{
    public sealed class EmailFieldCollection : FieldCollection<EmailField>
    {
        public EmailFieldCollection()
        {
        }

        public EmailFieldCollection(IEnumerable<EmailField> fields) : base(fields)
        {
        }
    }
}
