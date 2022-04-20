using System;

namespace RabbitOM.Net.Sdp.Validation
{
    /// <summary>
    /// Represent session descriptor validator
    /// </summary>
    public abstract class SessionDescriptorValidator
    {
        /// <summary>
        /// Represent the default validator
        /// </summary>
        public readonly static SessionDescriptorValidator DefaultValidator = new DefaultSessionDescriptorValidator();


        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        public abstract void Validate(SessionDescriptor descriptor);

        /// <summary>
        /// Validate          
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool TryValidate(SessionDescriptor descriptor);
    }
}
