using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved
{
    using RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved.Adapters;

    public sealed class CacheControlRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Cache-Control";

        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;



        private readonly StringRtspDictionary _extensions = new StringRtspDictionary();
        


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
        


        


        public bool AddExtension( string name , long value )
        {
            return AddExtension( name , value.ToString() );
        }

        public bool AddExtension( string name , string value )
        {
            if ( ! RtspHeaderValueValidator.TryValidate( name ) || ! RtspHeaderValueValidator.TryValidate( value ) )
            {
                return false;
            }

            return _extensions.TryAdd( ValueAdapter.Adapt( name ) , ValueAdapter.Adapt( value ) );
        }

        public bool RemoveExtension( string name )
        {
            return _extensions.Remove( ValueAdapter.Adapt( name ) );
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
                builder.Append( "no-cache, " );
            }

            if ( NoStore )
            {
                builder.Append( "no-store, " );
            }

            if ( NoTransform )
            {
                builder.Append( "no-transform, " );
            }

            if ( MustRevalidate )
            {
                builder.Append( "must-revalidate, " );
            }

            if ( ProxyRevalidate )
            {
                builder.Append( "proxy-revalidate, " );
            }

            if ( Public )
            {
                builder.Append( "public, " );
            }

            if ( Private )
            {
                builder.Append( "private, " );
            }

            if ( Immutable )
            {
                builder.Append( "immutable, " );
            }

            if ( MaximumAge.HasValue )
            {
                builder.AppendFormat( "max-age={0}, " , MaximumAge );
            }

            if ( ShareMaximumAge.HasValue )
            {
                builder.AppendFormat( "s-maxage={0}, " , ShareMaximumAge );
            }

            if ( StaleWhileRevalidate.HasValue )
            {
                builder.AppendFormat( "stale-while-revalidate={0}, " , StaleWhileRevalidate );
            }

            if ( StaleIfError.HasValue )
            {
                builder.AppendFormat( "stale-if-error={0}, " , StaleIfError );
            }

            foreach ( var extension in _extensions )
            { 
                if ( long.TryParse( extension.Value , out var _ ) )
                {
                    builder.AppendFormat( "{0}={1}, " , extension.Key , extension.Value );
                }
                else if ( double.TryParse( extension.Value , NumberStyles.Float, CultureInfo.InvariantCulture , out var _ ) )
                {
                    builder.AppendFormat( "{0}={1} ," , extension.Key , extension.Value );
                }
                else
                {
                    builder.AppendFormat( "{0}=\"{1}\" ," , extension.Key , extension.Value );
                }
            }

            return builder.ToString().Trim( ' ' , ',' );
        }
        



        public static bool TryParse( string input , out CacheControlRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new CacheControlRtspHeader();

                foreach ( var token in tokens.Where( element => ! element.Contains( "=" ) ) )
                {
                    if ( ValueComparer.Equals( "no-cache" , token ) )
                    {
                        header.NoCache = true;
                    }
                    else if ( ValueComparer.Equals( "no-store" , token ) )
                    {
                        header.NoStore = true;
                    }
                    else if ( ValueComparer.Equals( "no-transform" , token ) )
                    {
                        header.NoTransform = true;
                    }
                    else if ( ValueComparer.Equals( "must-revalidate" , token ) )
                    {
                        header.MustRevalidate = true;
                    }
                    else if ( ValueComparer.Equals( "proxy-revalidate" , token ) )
                    {
                        header.ProxyRevalidate = true;
                    }
                    else if ( ValueComparer.Equals( "public" , token ) )
                    {
                        header.Public = true;
                    }
                    else if ( ValueComparer.Equals( "private" , token ) )
                    {
                        header.Private = true;
                    }
                    else if ( ValueComparer.Equals( "immutable" , token ) )
                    {
                        header.Immutable = true;
                    }
                }

                foreach ( var token in tokens.Where( element => element.Contains( "=" ) ) )
                {
                    if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "max-age" , parameter.Key ) )
                        {
                            if ( int.TryParse( parameter.Value , out var number ) )
                            {
                                header.MaximumAge = number;
                            }
                        }
                        else if ( ValueComparer.Equals( "s-maxage" , parameter.Key ) )
                        {
                            if ( int.TryParse( parameter.Value , out var number ) )
                            {
                                header.ShareMaximumAge = number;
                            }
                        }
                        else if ( ValueComparer.Equals( "stale-while-revalidate" , parameter.Key ) )
                        {
                            if ( int.TryParse( parameter.Value , out var number ) )
                            {
                                header.StaleWhileRevalidate = number;
                            }
                        }
                        else if ( ValueComparer.Equals( "stale-if-error" , parameter.Key ) )
                        {
                            if ( int.TryParse( parameter.Value , out var number ) )
                            {
                                header.StaleIfError = number;
                            }
                        }
                        else
                        {
                            header.AddExtension( parameter.Key , parameter.Value );
                        }
                    }
                }

                result = header;
            }

            return result != null;
        }
    }
}
