using RabbitOM.Streaming.Net.Sdp.Validation;
using System;

namespace RabbitOM.Streaming.Net.Sdp
{
    /// <summary>
    /// Represent a sdp field
    /// </summary>
    public abstract class BaseField	
    {
        /// <summary>
        /// Gets the type name
        /// </summary>
        public abstract string TypeName
        {
            get;
        }




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
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool TryValidate();

        /// <summary>
        /// Format to a string
        /// </summary>
        /// <returns>returns a string</returns>
        public abstract override string ToString();
    }
}
