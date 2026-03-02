using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public sealed class IfMatchRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "If-Match";






        private readonly HashSet<WeightedString> _entitiesTags = new HashSet<WeightedString>();







        public IReadOnlyCollection<WeightedString> EntitiesTags
        {
            get => _entitiesTags;
        }









        public bool AddEntityTag( WeightedString etag )
        {
            if ( WeightedString.IsNullOrEmpty( etag ) )
            {
                return false;
            }

            return _entitiesTags.Add( etag );
        }

        public bool RemoveEntityTag( WeightedString etag )
        {
            return _entitiesTags.Remove( etag );
        }

        public void ClearEntitiesTags()
        {
            _entitiesTags.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _entitiesTags );
        }








        public static bool TryParse( string input , out IfMatchRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , "," , out var tokens ) )
            {
                var header = new IfMatchRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( WeightedString.TryParse( token , out var etag ) )
                    {
                        header.AddEntityTag( etag );
                    }
                }
            
                if ( header.EntitiesTags.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
