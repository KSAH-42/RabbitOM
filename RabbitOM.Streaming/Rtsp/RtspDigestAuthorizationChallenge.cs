﻿using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an authorization token generator
    /// </summary>
    internal abstract class RtspDigestAuthorizationChallenge : RtspAuthorizationChallenge
    {
        private RtspMethod               _method      = RtspMethod.UnDefined;

        private string                   _uri         = string.Empty;

        private string                   _realm       = string.Empty;

        private string                   _nonce       = string.Empty;





        /// <summary>
        /// Gets / Sets the method
        /// </summary>
        public RtspMethod Method
        {
            get => _method;
            set => _method = value;
        }

        /// <summary>
        /// Gets / Sets the nonce
        /// </summary>
        public string Uri
        {
            get => _uri;
            set => _uri = value ?? string.Empty;
        }

        /// <summary>
        /// Gets / Sets the realm
        /// </summary>
        public string Realm
        {
            get => _realm;
            set => _realm = value ?? string.Empty;
        }

        /// <summary>
        /// Gets / Sets the nonce
        /// </summary>
        public string Nonce
        {
            get => _nonce;
            set => _nonce = value ?? string.Empty;
        }





        /// <summary>
        /// Perform a validation
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _method != RtspMethod.UnDefined
                
                && ! string.IsNullOrWhiteSpace( _uri   )
                && ! string.IsNullOrWhiteSpace( _realm )
                && ! string.IsNullOrWhiteSpace( _nonce )
         
                && base.TryValidate()
                ;
        }
    }
}
