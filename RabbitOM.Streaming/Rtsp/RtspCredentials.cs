using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the credentials class
    /// </summary>
    public sealed class RtspCredentials
    {
        /// <summary>
        /// Represent an empty 
        /// </summary>
        public readonly static RtspCredentials Empty = new RtspCredentials();




        private readonly string _userName = string.Empty;

        private readonly string _password = string.Empty;




        /// <summary>
        /// Constructor
        /// </summary>
        private RtspCredentials()
        {
        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <param name="password">the password</param>
        public RtspCredentials( string userName , string password )
        {
            _userName = userName ?? string.Empty;
            _password = password ?? string.Empty;
        }




        /// <summary>
        /// Gets the user name
        /// </summary>
        public string UserName
        {
            get => _userName;
        }

        /// <summary>
        /// Gets the password
        /// </summary>
        public string Password
        {
            get => _password;
        }




        /// <summary>
        /// Check if the user name and password has been set
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        internal bool TryValidate()
        {
            return !string.IsNullOrWhiteSpace( _userName )
                && !string.IsNullOrWhiteSpace( _password );
        }
    }
}
