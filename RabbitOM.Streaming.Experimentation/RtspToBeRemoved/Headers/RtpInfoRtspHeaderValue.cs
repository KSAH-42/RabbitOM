using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Types;

    public sealed class RtpInfoRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "RTP-Info";

        private readonly HashSet<RtpInfo> _rtpInfos = new HashSet<RtpInfo>();


        public IReadOnlyCollection<RtpInfo> RtpInfos
        {
            get => _rtpInfos;
        }


        public static bool TryParse( string input , out RtpInfoRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new RtpInfoRtspHeaderValue();

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

        public void RemoveRtpInfos()
        {
            _rtpInfos.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _rtpInfos );
        }
    }
}
