using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class ViaRtspHeader 
    { 
        public static readonly string TypeName = "Via";
        



        private readonly HashSet<RtspProxy> _proxies = new HashSet<RtspProxy>();




        public IReadOnlyCollection<RtspProxy> Proxies
        {
            get => _proxies;
        }
        



        public static bool TryParse( string input , out ViaRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ',' , out var tokens ) )
            {
                var header = new ViaRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( RtspProxy.TryParse( token , out var proxy ) )
                    {
                        header.AddProxy( proxy );
                    }
                }

                if ( header.Proxies.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }




        public bool AddProxy( RtspProxy proxy )
        {
            if ( proxy != null )
            {
                return _proxies.Add( proxy );
            }

            return false;
        }

        public bool RemoveProxy( RtspProxy proxy )
        {
            return _proxies.Remove( proxy );
        }

        public void RemoveProxies()
        {
            _proxies.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _proxies );
        }
    }
}
