using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent the collection for bandwith field
    /// </summary>
    public sealed class BandwithFieldCollection : FieldCollection<BandwithField> 
    {
        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        public BandwithFieldCollection()
        {
        }

        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        /// <param name="fields">the collection of field</param>
        public BandwithFieldCollection(IEnumerable<BandwithField> fields)
            : base(fields)
        {
        }
    }
}
