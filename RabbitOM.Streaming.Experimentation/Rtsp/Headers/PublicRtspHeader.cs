using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class PublicRtspHeader 
    {
        public static readonly string TypeName = "Allow";





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

        public void RemoveMethods()
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

            if ( RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new PublicRtspHeader();

                foreach( var token in tokens )
                {
                    if ( RtspMethod.TryParse( RtspHeaderValueNormalizer.Normalize( token ) , out var method ) )
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
