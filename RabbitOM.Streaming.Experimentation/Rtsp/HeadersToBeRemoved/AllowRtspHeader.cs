using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved
{
    public sealed class AllowRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Allow";

        private readonly HashSet<RtspMethod> _methods = new HashSet<RtspMethod>();

        public static bool TryParse( string input , out AllowRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
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

            return _methods.Remove( _methods.FirstOrDefault( predicate ) );
        }

        public void ClearMethods()
        {
            _methods.Clear();
        }
        
        public override string ToString()
        {
            return string.Join( ", " , _methods );
        }
    }
}
