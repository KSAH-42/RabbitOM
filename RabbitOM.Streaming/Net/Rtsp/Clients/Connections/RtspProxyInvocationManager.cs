using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients.Connections
{
    using RabbitOM.Streaming.Net.Rtsp.Clients.Connections.Invokers;

    /// <summary>
    /// Represent the invoker manager. This class is used to handle the creation of invoker depending to proxy configuration. It's really looks an instance factory class but it must for the future implement optimization code
    /// </summary>
    /// <remarks>
    ///   <para>Actually this instance factory class doesn't cache existing invoker, but if one day you need to deliver only a single instance of an invoker for performance reasons or for any others aspects that required the creation and return an unique instance of an invoker, please modify the implementation of this class and add use a singleton to control the number of reference, or perphaps used a concurrent dictionnary (or a custom collection) to store the reference of an invoker.</para>
    ///   <para>If you need to initialize an invoker before to return the desired instance, avoid to add code on the constructor of the invoker, please prefer to add initialization code on this factory class.</para>
    /// </remarks>
    internal sealed class RtspProxyInvocationManager
    {
        private readonly RtspProxy _proxy = null;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspProxyInvocationManager( RtspProxy proxy )
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }






        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateOptionsInvoker()
        {
            return new RtspOptionsInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateDescribeInvoker()
        {
            return new RtspDescribeInvoker( _proxy ).SetHeaderAcceptSdp();
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateSetupInvoker()
        {
            return new RtspSetupInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreatePlayInvoker()
        {
            return new RtspPlayInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreatePauseInvoker()
        {
            return new RtspPauseInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateTearDownInvoker()
        {
            return new RtspTearDownInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateKeepAliveInvoker()
        {
            return CreateKeepAliveInvoker(RtspKeepAliveType.Options);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <param name="keepAliveType">the method</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentException"/>
        public IRtspInvoker CreateKeepAliveInvoker(RtspKeepAliveType keepAliveType )
        {
            if (keepAliveType == RtspKeepAliveType.Options)
            {
                return new RtspKeepAliveInvoker( _proxy , RtspMethod.Options );
            }

            if ( keepAliveType == RtspKeepAliveType.GetParameter )
            {
                return new RtspKeepAliveInvoker(_proxy, RtspMethod.GetParameter);
            }

            if ( keepAliveType == RtspKeepAliveType.SetParameter )
            {
                return new RtspKeepAliveInvoker(_proxy, RtspMethod.SetParameter);
            }

            throw new ArgumentException( "Unknow type" , nameof( keepAliveType ) );
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateGetParameterInvoker()
        {
            return new RtspGetParameterInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateSetParameterInvoker()
        {
            return new RtspSetParameterInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateRecordInvoker()
        {
            return new RtspRecordInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateAnnounceInvoker()
        {
            return new RtspAnnounceInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRtspInvoker CreateRedirectInvoker()
        {
            return new RtspRedirectInvoker(_proxy);
        }
    }
}
