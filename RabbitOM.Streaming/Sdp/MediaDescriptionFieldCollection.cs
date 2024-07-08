﻿using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent the collection for media description field
    /// </summary>
    public sealed class MediaDescriptionFieldCollection : FieldCollection<MediaDescriptionField>
    {
        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        public MediaDescriptionFieldCollection()
        {
        }

        /// <summary>
        /// Initialize a new instance of the collection
        /// </summary>
        /// <param name="fields">the collection of field</param>
        public MediaDescriptionFieldCollection(IEnumerable<MediaDescriptionField> fields)
            : base(fields)
        {																		
        }
    }
}
