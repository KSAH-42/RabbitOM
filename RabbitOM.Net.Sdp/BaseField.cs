using System;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent the sdp field
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
        /// <returns>returns true for a success, otherwise false</returns>
        public abstract bool Validate();
    }
}
