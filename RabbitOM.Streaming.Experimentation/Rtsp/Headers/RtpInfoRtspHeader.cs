using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RtpInfoRtspHeader
    {
        public static readonly string TypeName = "RTP-Info";

        private readonly HashSet<RtpInfo> _rtpInfos = new HashSet<RtpInfo>();


        public IReadOnlyCollection<RtpInfo> RtpInfos
        {
            get => _rtpInfos;
        }


        public static bool TryParse( string input , out RtpInfoRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
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

        public bool RemoveRtpInfoBy( Func<RtpInfo,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );

            }
            return _rtpInfos.Remove( _rtpInfos.FirstOrDefault( predicate ) );
        }

        public void ClearRtpInfos()
        {
            _rtpInfos.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _rtpInfos );
        }
    }
}
