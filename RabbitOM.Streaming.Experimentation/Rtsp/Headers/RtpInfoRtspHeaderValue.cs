using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RtpInfoRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "RTP-Info";





        private readonly HashSet<RtpInfo> _infos = new HashSet<RtpInfo>();






        public IReadOnlyCollection<RtpInfo> Infos
        {
            get => _infos;
        }


        public bool AddInfo( RtpInfo info )
        {
            if ( info == null )
            {
                return false;
            }

            return _infos.Add( info );
        }

        public bool RemoveInfo( RtpInfo info )
        {
            return _infos.Remove( info );
        }

        public void RemoveInfos()
        {
            _infos.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _infos );
        }


                
        
        
        public static bool TryParse( string input , out RtpInfoRtspHeaderValue result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new RtpInfoRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( RtpInfo.TryParse( token , out var info ) )
                    {
                        header.AddInfo( info );
                    }
                }

                if ( header.Infos.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
