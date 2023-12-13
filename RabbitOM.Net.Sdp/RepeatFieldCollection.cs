using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the collection for repeat field
    /// </summary>
    public sealed class RepeatFieldCollection : FieldCollection<RepeatField>
    {
        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        public RepeatFieldCollection()
        {
        }

        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        /// <param name="fields">the collection of field</param>
        public RepeatFieldCollection(IEnumerable<RepeatField> fields)
            : base(fields)
        {
        }
    }
}
