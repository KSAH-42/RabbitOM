using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent the collection for time field
    /// </summary>
    public sealed class TimeFieldCollection : FieldCollection<TimeField>
    {
        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        public TimeFieldCollection()
        {
        }

        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        /// <param name="fields">the collection of field</param>
        public TimeFieldCollection(IEnumerable<TimeField> fields)
            : base(fields)
        {
        }
    }
}
