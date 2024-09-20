using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal abstract class RtspAuthorizationChallenge
    {
        private string _userName = string.Empty;

        private string _password = string.Empty;





        /// <summary>
        /// Gets / Sets the user name
        /// </summary>
        public string UserName
        {
            get => _userName;
            set => _userName = value ?? string.Empty;
        }

        /// <summary>
        /// Gets / Sets the password
        /// </summary>
        public string Password
        {
            get => _password;
            set => _password = value ?? string.Empty;
        }





        /// <summary>
        /// Try to perform a validation
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public virtual bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _userName ) 
                && ! string.IsNullOrWhiteSpace( _password )
                ;
        }

        /// <summary>
        /// Create an authorization
        /// </summary>
        /// <returns>returns a value</returns>
        public abstract string CreateAuthorization();
    }
}
