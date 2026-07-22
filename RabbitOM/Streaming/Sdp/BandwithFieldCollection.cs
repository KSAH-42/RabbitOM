using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp
{
    public sealed class BandwithFieldCollection : FieldCollection<BandwithField>
    {
        public BandwithFieldCollection()
        {
        }

        public BandwithFieldCollection(IEnumerable<BandwithField> fields) : base(fields)
        {
        }
    }
}
