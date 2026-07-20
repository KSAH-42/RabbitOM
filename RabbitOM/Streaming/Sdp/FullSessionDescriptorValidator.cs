using System;

namespace RabbitOM.Streaming.Sdp
{
    /// <summary>
    /// Represent session descriptor validator which validate ALL fields presents in the descriptor
    /// </summary>
    public sealed class FullSessionDescriptorValidator : SessionDescriptionValidator
    {
        /// <summary>
        /// Initialiaze a new instance of validator
        /// </summary>
        internal FullSessionDescriptorValidator()
        {
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <exception cref="ArgumentNullException"/>
        public override void Validate(SessionDescription descriptor)
        {
            if ( descriptor == null )
            {
                throw new ArgumentNullException( nameof( descriptor ) );
            }
                                                  
            foreach ( var field in SessionDescription.EnumerateFields( descriptor ) )
            {
                field.Validate();
            }
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate(SessionDescription descriptor)
        {
            if ( descriptor == null )
            {
                return false;
            }

            foreach ( var field in SessionDescription.EnumerateFields( descriptor ) )
            {
                if ( ! field.TryValidate() )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
