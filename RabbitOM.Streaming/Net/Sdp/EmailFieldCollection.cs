using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Sdp
{
    /// <summary>
    /// Represent the collection for email field
    /// </summary>
    public sealed class EmailFieldCollection : FieldCollection<EmailField>
    {
        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        public EmailFieldCollection()
        {
        }

        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        /// <param name="fields">the collection of field</param>
        public EmailFieldCollection(IEnumerable<EmailField> fields)
            : base(fields)
        {
        }
    }
}
