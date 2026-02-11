using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class CacheControlRtspHeader : RtspHeader 
    {
        public const string TypeName = "Cache-Control";
        

        private readonly List<string> _privateHeaders = new List<string>();
        
        private readonly List<string> _noCacheHeaders = new List<string>();

        private readonly List<Extension> _extensions = new List<Extension>();




        public bool ProxyRevalidate { get; set; }
        public bool Private { get; set; }
        public bool OnlyIfCached { get; set; }
        public bool NoTransform { get; set; }
        public bool NoStore { get; set; }
        public bool NoCache { get; set; }
        public bool MustRevalidate { get; set; }
        public TimeSpan? MinFresh { get; set; }
        public TimeSpan? MaxStaleLimit { get; set; }
        public bool MaxStale { get; set; }
        public TimeSpan? MaxAge { get; set; }
        public bool Public { get; set; }
        public TimeSpan? SharedMaxAge { get; set; }
        public IReadOnlyCollection<string> PrivateHeaders { get => _privateHeaders; }
        public IReadOnlyCollection<string> NoCacheHeaders { get => _noCacheHeaders; }
        public IReadOnlyCollection<Extension> Extensions { get => _extensions; }
        


        public override bool TryValidate()
        {
            throw new NotImplementedException();
        }

        public void AddPrivateHeader( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            _privateHeaders.Add( value );
        }

        public void RemovePrivateHeader( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return;
            }

            _privateHeaders.Add( value );
        }

        public void RemoveAllPrivatesHeaders()
        {
            _privateHeaders.Clear();
        }

        public void AddCacheHeader( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentNullException( value );
            }

            _noCacheHeaders.Add( value );
        }

        public void RemoveCacheHeader( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return;
            }

            _noCacheHeaders.Add( value );
        }

        public void RemoveAllCachesHeaders()
        {
            _noCacheHeaders.Clear();
        }

        public void AddExtension( Extension value )
        {
            _extensions.Add( value ?? throw new ArgumentNullException( nameof( value ) ) );
        }

        public void RemoveExtension( Extension value )
        {
            if ( value == null )
            {
                return;
            }

            _extensions.Add( value );
        }

        public void RemoveAllExtensions()
        {
            _extensions.Clear();
        }        




        public override string ToString()
        {
            throw new NotImplementedException();
        }
        
        public static bool TryParse( string value , out CacheControlRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
