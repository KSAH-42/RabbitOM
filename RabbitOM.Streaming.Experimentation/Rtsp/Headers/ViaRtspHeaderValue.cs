using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ViaRtspHeaderValue : RtspHeaderValue
    { 
        public static readonly string TypeName = "Via";
        






        private readonly HashSet<ProxyInfo> _proxies = new HashSet<ProxyInfo>();







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

        public void RemoveProxies()
        {
            _proxies.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _proxies );
        }





        public static bool TryParse( string input , out ViaRtspHeaderValue result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new ViaRtspHeaderValue();

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
