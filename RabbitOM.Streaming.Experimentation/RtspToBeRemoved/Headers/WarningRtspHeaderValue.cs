using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers.Types;
    
    public sealed class WarningRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Warning";


        private readonly HashSet<WarningInfo> _infos = new HashSet<WarningInfo>();
                

        public IReadOnlyCollection<WarningInfo> Infos
        {
            get => _infos;
        }
        
        public static bool TryParse( string input , out WarningRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new WarningRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( WarningInfo.TryParse( token , out var warning ) )
                    {
                        header.AddInfo( warning );
                    }
                }

                if ( header.Infos.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public bool AddInfo( WarningInfo warning )
        {
            if ( warning != null )
            {
                return _infos.Add( warning );
            }

            return false;
        }

        public bool RemoveInfo( WarningInfo warning )
        {
            return _infos.Remove( warning );
        }

        public void RemoveInfos()
        {
            _infos.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _infos );
        }
    }
}
