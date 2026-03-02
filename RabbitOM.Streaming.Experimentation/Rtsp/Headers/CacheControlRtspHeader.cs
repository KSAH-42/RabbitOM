using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class CacheControlRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Cache-Control";






        private readonly Dictionary<string,string> _extensions = new Dictionary<string, string>();






        public bool NoCache { get; set; }

        public bool NoStore { get; set; }

        public bool NoTransform { get; set; }
        
        public bool MustRevalidate { get; set; }
        
        public bool ProxyRevalidate { get; set; }

        public bool Public { get; set; }

        public bool Private { get; set; }

        public bool Immutable { get; set; }

        public int? MaximumAge { get; set; }

        public int? ShareMaximumAge { get; set; }

        public int? StaleWhileRevalidate { get; set; }

        public int? StaleIfError { get; set; }

        public IReadOnlyDictionary<string,string> Extensions { get => _extensions; }







        public void SetNoCache( string value )
        {
            NoCache = bool.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetNoStore( string value )
        {
            NoStore = bool.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetNoTransform( string value )
        {
            NoTransform = bool.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? result 
                : false
                ;
        }
        
        public void SetMustRevalidate( string value )
        {
            MustRevalidate = bool.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? result 
                : false
                ;
        }
        
        public void SetProxyRevalidate( string value )
        {
            ProxyRevalidate = bool.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetPublic( string value )
        {
            Public = bool.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetPrivate( string value )
        {
            Private = bool.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetImmutable( string value )
        {
            Immutable = bool.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetMaximumAge( string value )
        {
            MaximumAge = int.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? new int?( result )
                : null
                ;
        }

        public void SetShareMaximumAge( string value )
        {
            ShareMaximumAge = int.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? new int?( result )
                : null
                ;
        }

        public void SetStaleWhileRevalidate( string value )
        {
            StaleWhileRevalidate = int.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? new int?( result )
                : null
                ;
        }

        public void SetStaleIfError( string value )
        {
            StaleIfError = int.TryParse( RtspHeaderParser.Formatter.Filter( value ) , out var result )
                ? new int?( result )
                : null
                ;
        }

        public bool AddExtension( string name , long value )
        {
            return AddExtension( name , value.ToString() );
        }

        public bool AddExtension( string name , string value )
        {
            var extensionName = RtspHeaderParser.Formatter.Filter( name );

            if ( string.IsNullOrWhiteSpace( extensionName ) )
            {
                return false;
            }

            _extensions[ extensionName ] = RtspHeaderParser.Formatter.Filter( value );

            return true;
        }

        public bool RemoveExtensionByName( string name )
        {
            return _extensions.Remove( RtspHeaderParser.Formatter.Filter( name ) );
        }

        public void ClearExtensions()
        {
            _extensions.Clear();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( NoCache )
            {
                builder.Append( "no-cache," );
            }

            if ( NoStore )
            {
                builder.Append( "no-store," );
            }

            if ( NoTransform )
            {
                builder.Append( "no-transform," );
            }

            if ( MustRevalidate )
            {
                builder.Append( "must-revalidate," );
            }

            if ( ProxyRevalidate )
            {
                builder.Append( "proxy-revalidate," );
            }

            if ( Public )
            {
                builder.Append( "public," );
            }

            if ( Private )
            {
                builder.Append( "private," );
            }

            if ( Immutable )
            {
                builder.Append( "immutable," );
            }

            if ( MaximumAge.HasValue )
            {
                builder.AppendFormat( "max-age={0}," , MaximumAge );
            }

            if ( ShareMaximumAge.HasValue )
            {
                builder.AppendFormat( "s-maxage={0}," , ShareMaximumAge );
            }

            if ( StaleWhileRevalidate.HasValue )
            {
                builder.AppendFormat( "stale-while-revalidate={0}," , StaleWhileRevalidate );
            }

            if ( StaleIfError.HasValue )
            {
                builder.AppendFormat( "stale-if-error={0}," , StaleIfError );
            }

            foreach ( var extension in _extensions )
            { 
                if ( long.TryParse( extension.Value , out var _ ) )
                {
                    builder.AppendFormat( "{0}={1}," , extension.Key , extension.Value );
                }
                else if ( double.TryParse( extension.Value , NumberStyles.Float, CultureInfo.InvariantCulture , out var _ ) )
                {
                    builder.AppendFormat( "{0}={1}," , extension.Key , extension.Value );
                }
                else
                {
                    builder.AppendFormat( "{0}=\"{1}\"," , extension.Key , extension.Value );
                }
            }

            return builder.ToString().Trim( ' ' , ',' );
        }






        public static bool TryParse( string input , out CacheControlRtspHeader result )
        {
            result = null;

            var comparer = StringComparer.OrdinalIgnoreCase;

            if ( RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , "," , out var tokens ) )
            {
                var header = new CacheControlRtspHeader();

                foreach ( var token in tokens.Where( element => ! element.Contains( "=" ) ) )
                {
                    if ( comparer.Equals( "no-cache" , token ) )
                    {
                        header.NoCache = true;
                    }
                    else if ( comparer.Equals( "no-store" , token ) )
                    {
                        header.NoStore = true;
                    }
                    else if ( comparer.Equals( "no-transform" , token ) )
                    {
                        header.NoTransform = true;
                    }
                    else if ( comparer.Equals( "must-revalidate" , token ) )
                    {
                        header.MustRevalidate = true;
                    }
                    else if ( comparer.Equals( "proxy-revalidate" , token ) )
                    {
                        header.ProxyRevalidate = true;
                    }
                    else if ( comparer.Equals( "public" , token ) )
                    {
                        header.Public = true;
                    }
                    else if ( comparer.Equals( "private" , token ) )
                    {
                        header.Private = true;
                    }
                    else if ( comparer.Equals( "immutable" , token ) )
                    {
                        header.Immutable = true;
                    }
                }

                foreach ( var token in tokens.Where( element => element.Contains( "=" ) ) )
                {
                    if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                    {
                        if ( comparer.Equals( "max-age" , parameter.Name ) )
                        {
                            header.SetMaximumAge( parameter.Value );
                        }
                        else if ( comparer.Equals( "s-maxage" , parameter.Name ) )
                        {
                            header.SetShareMaximumAge( parameter.Value );
                        }
                        else if ( comparer.Equals( "stale-while-revalidate" , parameter.Name ) )
                        {
                            header.SetStaleWhileRevalidate( parameter.Value );
                        }
                        else if ( comparer.Equals( "stale-if-error" , parameter.Name ) )
                        {
                            header.SetStaleIfError( parameter.Value );
                        }
                        else
                        {
                            header.AddExtension( parameter.Name , parameter.Value );
                        }
                    }
                }

                result = header;
            }

            return result != null;
        }
    }
}
