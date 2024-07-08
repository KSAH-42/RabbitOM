using RabbitOM.Streaming.Sdp.Validation;
using System;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent the sdp value content
    /// </summary>
    public abstract class AttributeValue
    {
        /// <summary>
        /// Validate
        /// </summary>
        /// <exception cref="ValidationException"/>
        public virtual void Validate()
        {
            if ( ! TryValidate() )
            {
                throw new ValidationException();
            }
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool TryValidate();
    }
}
