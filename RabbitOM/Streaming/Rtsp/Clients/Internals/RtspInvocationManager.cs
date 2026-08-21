using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspInvocationManager
    {
        private readonly RtspProxy _proxy = null;

        public RtspInvocationManager( RtspProxy proxy )
        {
            _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
        }

        public IRtspInvoker CreateOptionsInvoker()
        {
            return new RtspOptionsInvoker( _proxy );
        }

        public IRtspInvoker CreateDescribeInvoker()
        {
            return new RtspDescribeInvoker( _proxy ).SetHeaderAcceptSdp();
        }

        public IRtspInvoker CreateSetupInvoker()
        {
            return new RtspSetupInvoker( _proxy );
        }

        public IRtspInvoker CreatePlayInvoker()
        {
            return new RtspPlayInvoker( _proxy );
        }

        public IRtspInvoker CreatePauseInvoker()
        {
            return new RtspPauseInvoker( _proxy );
        }

        public IRtspInvoker CreateTearDownInvoker()
        {
            return new RtspTearDownInvoker( _proxy );
        }

        public IRtspInvoker CreateKeepAliveInvoker()
        {
            return CreateKeepAliveInvoker( RtspKeepAliveType.Options );
        }

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

        public IRtspInvoker CreateGetParameterInvoker()
        {
            return new RtspGetParameterInvoker( _proxy );
        }

        public IRtspInvoker CreateSetParameterInvoker()
        {
            return new RtspSetParameterInvoker( _proxy );
        }

        public IRtspInvoker CreateRecordInvoker()
        {
            return new RtspRecordInvoker( _proxy );
        }

        public IRtspInvoker CreateAnnounceInvoker()
        {
            return new RtspAnnounceInvoker( _proxy );
        }

        public IRtspInvoker CreateRedirectInvoker()
        {
            return new RtspRedirectInvoker( _proxy );
        }
    }
}
