using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Sdp
{
    public sealed class MediaDescriptionFieldCollection : FieldCollection<MediaDescriptionField>
    {
        public MediaDescriptionFieldCollection()
        {
        }

        public MediaDescriptionFieldCollection(IEnumerable<MediaDescriptionField> fields) : base(fields)
        {
        }
    }
}
