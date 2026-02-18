using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;
    
    public sealed class CacheControlRtspHeader
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





        public static bool TryParse( string input , out CacheControlRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , ',' , out var tokens ) )
            {
                var header = new CacheControlRtspHeader();

                foreach ( var token in tokens.Where( element => ! element.Contains( "=" ) ) )
                {
                    if ( string.Equals( "no-cache" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.NoCache = true;
                    }
                    else if ( string.Equals( "no-store" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.NoStore = true;
                    }
                    else if ( string.Equals( "no-transform" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.NoTransform = true;
                    }
                    else if ( string.Equals( "must-revalidate" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.MustRevalidate = true;
                    }
                    else if ( string.Equals( "proxy-revalidate" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.ProxyRevalidate = true;
                    }
                    else if ( string.Equals( "public" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.Public = true;
                    }
                    else if ( string.Equals( "private" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.Private = true;
                    }
                    else if ( string.Equals( "immutable" , token , StringComparison.OrdinalIgnoreCase ) )
                    {
                        header.Immutable = true;
                    }
                }

                foreach ( var token in tokens.Where( element => element.Contains( "=" ) ) )
                {
                    if ( StringParameterRtspHeaderParser.TryParse( token , '=' , out var parameter ) )
                    {
                        if ( string.Equals( "max-age" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetMaximumAge( parameter.Value );
                        }
                        else if ( string.Equals( "s-maxage" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetShareMaximumAge( parameter.Value );
                        }
                        else if ( string.Equals( "stale-while-revalidate" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SetStaleWhileRevalidate( parameter.Value );
                        }
                        else if ( string.Equals( "stale-if-error" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
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






        public void SetNoCache( string value )
        {
            NoCache = bool.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetNoStore( string value )
        {
            NoStore = bool.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetNoTransform( string value )
        {
            NoTransform = bool.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? result 
                : false
                ;
        }
        
        public void SetMustRevalidate( string value )
        {
            MustRevalidate = bool.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? result 
                : false
                ;
        }
        
        public void SetProxyRevalidate( string value )
        {
            ProxyRevalidate = bool.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetPublic( string value )
        {
            Public = bool.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetPrivate( string value )
        {
            Private = bool.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetImmutable( string value )
        {
            Immutable = bool.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? result 
                : false
                ;
        }

        public void SetMaximumAge( string value )
        {
            MaximumAge = int.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? new int?( result )
                : null
                ;
        }

        public void SetShareMaximumAge( string value )
        {
            ShareMaximumAge = int.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? new int?( result )
                : null
                ;
        }

        public void SetStaleWhileRevalidate( string value )
        {
            StaleWhileRevalidate = int.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
                ? new int?( result )
                : null
                ;
        }

        public void SetStaleIfError( string value )
        {
            StaleIfError = int.TryParse( RtspValueNormalizer.Normalize( value ) , out var result )
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
            var extensionName = RtspValueNormalizer.Normalize( name );

            if ( string.IsNullOrWhiteSpace( extensionName ) )
            {
                return false;
            }

            _extensions[ extensionName ] = RtspValueNormalizer.Normalize( value );

            return true;
        }

        public bool RemoveExtensinByName( string name )
        {
            return _extensions.Remove( RtspValueNormalizer.Normalize( name ) );
        }

        public void RemoveExtensions()
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
    }
}
