using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent an helper class used to safely invoke delegate
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Throw an exception if we are in release mode
        /// </summary>
        /// <param name="message">the message</param>
        /// <exception cref="NotImplementedException"/>
        public static void ThrowOnRelease( string message )
        {
#if !DEBUG
            throw new NotImplementedException( message );
#endif
        }
    }
}
