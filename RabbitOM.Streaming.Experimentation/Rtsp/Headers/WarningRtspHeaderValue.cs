using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;
    
    public sealed class WarningRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Warning";


        private readonly HashSet<WarningInfo> _warnings = new HashSet<WarningInfo>();
                

        public IReadOnlyCollection<WarningInfo> Warnings
        {
            get => _warnings;
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
                        header.AddWarning( warning );
                    }
                }

                if ( header.Warnings.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }

        public bool AddWarning( WarningInfo warning )
        {
            if ( warning != null )
            {
                return _warnings.Add( warning );
            }

            return false;
        }

        public bool RemoveWarning( WarningInfo warning )
        {
            return _warnings.Remove( warning );
        }

        public void RemoveWarnings()
        {
            _warnings.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _warnings );
        }
    }
}
