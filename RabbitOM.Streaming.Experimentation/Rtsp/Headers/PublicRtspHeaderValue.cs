using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class PublicRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Public";

        private readonly HashSet<RtspMethod> _methods = new HashSet<RtspMethod>();
        

        public IReadOnlyCollection<RtspMethod> Methods
        {
            get => _methods;
        }
        

        public static bool TryParse( string input , out PublicRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new PublicRtspHeaderValue();

                foreach( var token in tokens )
                {
                    if ( RtspMethod.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( token ) , out var method ) )
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
