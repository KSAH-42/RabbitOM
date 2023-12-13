using System;

namespace RabbitOM.Net.Sdp.Validation
{
    /// <summary>
    /// Represent session descriptor validator
    /// </summary>
    public sealed class DefaultSessionDescriptorValidator : SessionDescriptorValidator
    {
        /// <summary>
        /// Initialiaze a new instance of validator
        /// </summary>
        internal DefaultSessionDescriptorValidator()
        {
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <exception cref="ArgumentNullException"/>
        public override void Validate(SessionDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            descriptor.Version.Validate();
            descriptor.SessionName.Validate();
            descriptor.SessionInformation.Validate();
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate(SessionDescriptor descriptor)
        {
            if (descriptor == null)
            {
                return false;
            }

            return descriptor.Version.TryValidate()
                && descriptor.SessionName.TryValidate()
                && descriptor.SessionInformation.TryValidate();
        }
    }
}
