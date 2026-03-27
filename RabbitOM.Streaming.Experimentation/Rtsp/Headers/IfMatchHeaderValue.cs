using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class IfMatchHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;


        private readonly HashSet<string> _entitiesTags = new HashSet<string>( StringComparer.OrdinalIgnoreCase );




        public IReadOnlyCollection<string> EntitiesTags
        {
            get => _entitiesTags;
        }






        public static bool TryParse( string input , out IfMatchHeaderValue result )
        {
            result = null;

            if ( HeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new IfMatchHeaderValue();

                foreach ( var token in tokens )
                {
                    header.AddEntityTag( token );
                }
            
                if ( header.EntitiesTags.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }







        public bool AddEntityTag( string etag )
        {
            return AddEntityTag( etag , HeaderProtocolValidator.IsValidToken );
        }

        public bool AddEntityTag( string etag , Func<string,bool> validator )
        {
            if ( validator?.Invoke( etag = ValueNormalizer.Normalize( etag ) ) == true )
            {
                return _entitiesTags.Add( etag );
            }
            
            return false;
        }

        public bool RemoveEntityTag( string etag )
        {
            return _entitiesTags.Remove( ValueNormalizer.Normalize( etag ) );
        }

        public void RemoveEntitiesTags()
        {
            _entitiesTags.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _entitiesTags.Select( element => $"\"{element}\"" ) );
        }
    }
}
