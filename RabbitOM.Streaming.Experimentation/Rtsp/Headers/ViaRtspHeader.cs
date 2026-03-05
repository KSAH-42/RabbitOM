using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class ViaRtspHeader : RtspHeader
    { 
        private readonly HashSet<ProxyInfo> _proxies = new HashSet<ProxyInfo>();





        public static string TypeName { get; } = "Via";
        

        public IReadOnlyCollection<ProxyInfo> Proxies
        {
            get => _proxies;
        }
        



        
        public bool AddProxy( ProxyInfo proxy )
        {
            if ( proxy != null )
            {
                return _proxies.Add( proxy );
            }

            return false;
        }

        public bool RemoveProxy( ProxyInfo proxy )
        {
            return _proxies.Remove( proxy );
        }

        public void ClearProxies()
        {
            _proxies.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _proxies );
        }





        public static bool TryParse( string input , out ViaRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                var header = new ViaRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( ProxyInfo.TryParse( token , out var proxy ) )
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
    }
}
