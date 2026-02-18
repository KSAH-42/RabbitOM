using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class AllowRtspHeader 
    {
        public static readonly string TypeName = "Allow";



        private readonly HashSet<RtspMethod> _methods = new HashSet<RtspMethod>();
        

        
        public IReadOnlyCollection<RtspMethod> Methods
        {
            get => _methods;
        }
        
        
        
        public static bool TryParse( string input , out AllowRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ',' , out var tokens ) )
            {
                var header = new AllowRtspHeader();

                foreach( var token in tokens )
                {
                    if ( RtspMethod.TryParse( RtspValueNormalizer.Normalize( token ) , out var method ) )
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
        



        
        public bool AddMethod( RtspMethod method )
        {
            if ( method != null )
            {
                return _methods.Add( method );
            }

            return false;
        }

        public void RemoveMethod( RtspMethod method )
        {
            _methods.Remove( method );
        }

        public void RemoveMethods()
        {
            _methods.Clear();
        }
        
        public override string ToString()
        {
            return string.Join( ", " , _methods );
        }
    }
}
