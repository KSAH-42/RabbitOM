using System;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent session descriptor validator
    /// </summary>
    public sealed class DefaultSessionDescriptorValidator : SessionDescriptionValidator
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
        public override void Validate(SessionDescription descriptor)
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
        public override bool TryValidate(SessionDescription descriptor)
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
