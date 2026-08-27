using System;

namespace RabbitOM.Net.Rtsp.Clients
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
            return new OptionsRtspInvoker( _proxy );
        }

        public IRtspInvoker CreateDescribeInvoker()
        {
            return new DescribeRtspInvoker( _proxy ).SetHeaderAcceptSdp();
        }

        public IRtspInvoker CreateSetupInvoker()
        {
            return new SetupRtspInvoker( _proxy );
        }

        public IRtspInvoker CreatePlayInvoker()
        {
            return new PlayRtspInvoker( _proxy );
        }

        public IRtspInvoker CreatePauseInvoker()
        {
            return new PauseRtspInvoker( _proxy );
        }

        public IRtspInvoker CreateTearDownInvoker()
        {
            return new TearDownRtspInvoker( _proxy );
        }

        public IRtspInvoker CreateKeepAliveInvoker()
        {
            return CreateKeepAliveInvoker( RtspKeepAliveType.Options );
        }

        public IRtspInvoker CreateKeepAliveInvoker(RtspKeepAliveType keepAliveType )
        {
            if (keepAliveType == RtspKeepAliveType.Options)
            {
                return new KeepAliveRtspInvoker( _proxy , RtspMethod.Options );
            }

            if ( keepAliveType == RtspKeepAliveType.GetParameter )
            {
                return new KeepAliveRtspInvoker(_proxy, RtspMethod.GetParameter);
            }

            if ( keepAliveType == RtspKeepAliveType.SetParameter )
            {
                return new KeepAliveRtspInvoker(_proxy, RtspMethod.SetParameter);
            }

            throw new ArgumentException( "Unknow type" , nameof( keepAliveType ) );
        }

        public IRtspInvoker CreateGetParameterInvoker()
        {
            return new GetParameterRtspInvoker( _proxy );
        }

        public IRtspInvoker CreateSetParameterInvoker()
        {
            return new SetParameterRtspInvoker( _proxy );
        }

        public IRtspInvoker CreateRecordInvoker()
        {
            return new RecordRtspInvoker( _proxy );
        }

        public IRtspInvoker CreateAnnounceInvoker()
        {
            return new AnnounceRtspInvoker( _proxy );
        }

        public IRtspInvoker CreateRedirectInvoker()
        {
            return new RedirectRtspInvoker( _proxy );
        }
    }
}
