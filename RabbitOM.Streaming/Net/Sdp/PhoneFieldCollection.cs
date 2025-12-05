using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Sdp
{
    /// <summary>
    /// Represent the collection for phone field
    /// </summary>
    public sealed class PhoneFieldCollection : FieldCollection<PhoneField>
    {
        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        public PhoneFieldCollection()
        {
        }

        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        /// <param name="fields">the collection of field</param>
        public PhoneFieldCollection(IEnumerable<PhoneField> fields)
            : base(fields)
        {
        }
    }
}
