using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class RtpInfoRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "RTP-Info";





        private readonly HashSet<RtpInfo> _rtpInfos = new HashSet<RtpInfo>();






        public IReadOnlyCollection<RtpInfo> RtpInfos
        {
            get => _rtpInfos;
        }


        public bool AddRtpInfo( RtpInfo info )
        {
            if ( info == null )
            {
                return false;
            }

            return _rtpInfos.Add( info );
        }

        public bool RemoveRtpInfo( RtpInfo info )
        {
            return _rtpInfos.Remove( info );
        }

        public void ClearRtpInfos()
        {
            _rtpInfos.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _rtpInfos );
        }


                
        
        
        public static bool TryParse( string input , out RtpInfoRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                var header = new RtpInfoRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( RtpInfo.TryParse( token , out var info ) )
                    {
                        header.AddRtpInfo( info );
                    }
                }

                if ( header.RtpInfos.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
