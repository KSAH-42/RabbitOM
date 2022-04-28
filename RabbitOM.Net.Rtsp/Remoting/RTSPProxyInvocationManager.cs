using System;

namespace RabbitOM.Net.Rtsp.Remoting
{
    using RabbitOM.Net.Rtsp.Remoting.Invokers;

    /// <summary>
    /// Represent the invoker manager. This class is used to handle the creation of invoker depending to proxy configuration. It's really looks an instance factory class but it must for the future implement optimization code
    /// </summary>
    /// <remarks>
    ///   <para>Actually this instance factory class doesn't cache existing invoker, but if one day you need to deliver only a single instance of an invoker for performance reasons or for any others aspects that required the creation and return an unique instance of an invoker, please modify the implementation of this class and add use a singleton to control the number of reference, or perphaps used a concurrent dictionnary (or a custom collection) to store the reference of an invoker.</para>
    ///   <para>If you need to initialize an invoker before to return the desired instance, avoid to add code on the constructor of the invoker, please prefer to add initialization code on this factory class.</para>
    /// </remarks>
    public sealed class RTSPProxyInvocationManager
    {
        private readonly RTSPProxy _proxy = null;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxy">the proxy</param>
        /// <exception cref="ArgumentNullException"/>
		public RTSPProxyInvocationManager( RTSPProxy proxy )
		{
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
		}






        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateGetOptionsInvoker()
        {
            return new RTSPGetOptionsInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateDescribeInvoker()
        {
            return new RTSPDescribeInvoker( _proxy ).SetHeaderAcceptSdp();
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateSetupInvoker()
        {
            return new RTSPSetupInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreatePlayInvoker()
        {
            return new RTSPPlayInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreatePauseInvoker()
        {
            return new RTSPPauseInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateTearDownInvoker()
        {
            return new RTSPTearDownInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateKeepAliveInvoker()
		{
            return CreateKeepAliveInvoker(RTSPKeepAliveType.Options);
		}

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <param name="keepAliveType">the method</param>
        /// <returns>returns an instance</returns>
        /// <exception cref="ArgumentException"/>
        public IRTSPInvoker CreateKeepAliveInvoker(RTSPKeepAliveType keepAliveType )
        {
            if (keepAliveType == RTSPKeepAliveType.Options)
			{
                return new RTSPKeepAliveInvoker( _proxy , RTSPMethodType.Options );
			}

            if ( keepAliveType == RTSPKeepAliveType.GetParameter )
			{
                return new RTSPKeepAliveInvoker(_proxy, RTSPMethodType.GetParameter);
            }

            if ( keepAliveType == RTSPKeepAliveType.SetParameter )
			{
                return new RTSPKeepAliveInvoker(_proxy, RTSPMethodType.SetParameter);
            }

            throw new ArgumentException( "Unknow type" , nameof( keepAliveType ) );
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateGetParameterInvoker()
        {
            return new RTSPGetParameterInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateSetParameterInvoker()
        {
            return new RTSPSetParameterInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateRecordInvoker()
        {
            return new RTSPRecordInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateAnnounceInvoker()
        {
            return new RTSPAnnounceInvoker(_proxy);
        }

        /// <summary>
        /// Create an invoker
        /// </summary>
        /// <returns>returns an instance</returns>
        public IRTSPInvoker CreateRedirectInvoker()
		{
            return new RTSPRedirectInvoker(_proxy);
		}
    }
}
