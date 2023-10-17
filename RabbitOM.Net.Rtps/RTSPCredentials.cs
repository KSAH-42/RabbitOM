using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the credentials class
    /// </summary>
    public sealed class RTSPCredentials
    {
        /// <summary>
        /// Represent an empty 
        /// </summary>
        public readonly static RTSPCredentials Empty = new RTSPCredentials();




        private readonly string _userName = string.Empty;

        private readonly string _password = string.Empty;




        /// <summary>
        /// Constructor
        /// </summary>
        private RTSPCredentials()
        {
        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <param name="password">the password</param>
        public RTSPCredentials( string userName , string password )
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
        public bool TryValidate()
        {
            return !string.IsNullOrWhiteSpace( _userName )
                && !string.IsNullOrWhiteSpace( _password );
        }
    }
}
