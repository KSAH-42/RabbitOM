using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AllowRtspHeader : RtspHeader
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

        public bool RemoveMethodBy( Func<RtspMethod,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            var method = _methods.FirstOrDefault( predicate );

            if ( method == null )
            {
                return false;
            }

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
        



        
        
        public static bool TryParse( string input , out AllowRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                var header = new AllowRtspHeader();

                foreach( var token in tokens )
                {
                    if ( RtspMethod.TryParse( token , out var method ) )
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
