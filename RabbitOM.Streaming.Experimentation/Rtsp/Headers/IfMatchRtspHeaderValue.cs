using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class IfMatchRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "If-Match";






        private readonly HashSet<StringWithQuality> _entitiesTags = new HashSet<StringWithQuality>();







        public IReadOnlyCollection<StringWithQuality> EntitiesTags
        {
            get => _entitiesTags;
        }









        public bool AddEntityTag( StringWithQuality etag )
        {
            if ( StringWithQuality.IsNullOrEmpty( etag ) )
            {
                return false;
            }

            return _entitiesTags.Add( etag );
        }

        public bool RemoveEntityTag( StringWithQuality etag )
        {
            return _entitiesTags.Remove( etag );
        }

        public void RemoveEntitiesTags()
        {
            _entitiesTags.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _entitiesTags );
        }








        public static bool TryParse( string input , out IfMatchRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , "," , out var tokens ) )
            {
                var header = new IfMatchRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQuality.TryParse( token , out var etag ) )
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
