namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent session descriptor validator
    /// </summary>
    public abstract class SessionDescriptionValidator
    {
        /// <summary>
        /// Represent the default validator
        /// </summary>
        public readonly static SessionDescriptionValidator DefaultValidator = new DefaultSessionDescriptorValidator();

        /// <summary>
        /// Represent the full validator
        /// </summary>
        public readonly static SessionDescriptionValidator FullValidator    = new FullSessionDescriptorValidator();


        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        public abstract void Validate(SessionDescription descriptor);

        /// <summary>
        /// Validate          
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool TryValidate(SessionDescription descriptor);
    }
}
