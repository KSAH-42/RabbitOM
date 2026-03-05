using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class PublicRtspHeader : RtspHeader 
    {
        public static readonly string TypeName = "Public";





        private readonly HashSet<RtspMethod> _methods = new HashSet<RtspMethod>();
        



        
        public IReadOnlyCollection<RtspMethod> Methods
        {
            get => _methods;
        }
        
        
        
        
        
        public bool AddMethod( RtspMethod method )
        {
            if ( method != null )
            {
                return _methods.Add( method );
            }

            return false;
        }

        public bool RemoveMethod( RtspMethod method )
        {
            return _methods.Remove( method );
        }

        public void ClearMethods()
        {
            _methods.Clear();
        }
        
        public override string ToString()
        {
            return string.Join( ", " , _methods );
        }






        public static bool TryParse( string input , out PublicRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                var header = new PublicRtspHeader();

                foreach( var token in tokens )
                {
                    if ( RtspMethod.TryParse( StringRtspHeaderParser.UnQuote( token ) , out var method ) )
                    {
                        header.AddMethod( method );
                    }
                }

                if ( header.Methods.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
