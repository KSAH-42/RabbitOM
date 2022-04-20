using System;

namespace RabbitOM.Net.Sdp
{
    /// <summary>
    /// Represent a contract used for perform copy operations
    /// </summary>
    /// <typeparam name="TObject">the type of sdp field</typeparam>
    public interface ICopyable<TObject> where TObject : class
    {
        /// <summary>
        /// Perform a copy from a different instance object
        /// </summary>
        /// <param name="obj">the source used to perform a copy</param>
        void CopyFrom( TObject obj );
    }
}
