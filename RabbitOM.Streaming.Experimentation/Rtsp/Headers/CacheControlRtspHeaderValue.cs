using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes;

    public sealed class CacheControlRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;






        public bool NoCache { get; set; }

        public bool NoStore { get; set; }

        public bool NoTransform { get; set; }

        public bool MustRevalidate { get; set; }

        public bool ProxyRevalidate { get; set; }

        public bool Public { get; set; }

        public bool Private { get; set; }

        public bool Immutable { get; set; }

        public bool OnlyIfCached { get; set; }

        public int? MaximumAge { get; set; }

        public int? ShareMaximumAge { get; set; }

        public int? StaleWhileRevalidate { get; set; }

        public int? StaleIfError { get; set; }

        public int? MaximumStale { get; set; }

        public int? MinimumFresh { get; set; }

        public StringParameterRtspHeaderValueCollection Parameters { get; } = new StringParameterRtspHeaderValueCollection();







        public static bool TryParse( string input , out CacheControlRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new CacheControlRtspHeaderValue();

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
                    else if ( ValueComparer.Equals( "only-if-cached" , token ) )
                    {
                        header.OnlyIfCached = true;
                    }
                }

                foreach ( var token in tokens.Where( element => element.Contains( "=" ) ) )
                {
                    if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "max-age" , parameter.Key ) )
                        {
                            if ( int.TryParse( parameter.Value , out var number ) )
                            {
                                header.MaximumAge = number;
                            }
                        }
                        else if ( ValueComparer.Equals( "max-stale" , parameter.Key ) )
                        {
                            if ( int.TryParse( parameter.Value , out var number ) )
                            {
                                header.MaximumStale = number;
                            }
                        }
                        else if ( ValueComparer.Equals( "min-fresh" , parameter.Key ) )
                        {
                            if ( int.TryParse( parameter.Value , out var number ) )
                            {
                                header.MinimumFresh = number;
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
                            if ( StringParameterRtspHeaderValue.TryCreate( parameter.Key , parameter.Value , out var optionalParameter ) )
                            {
                                header.Parameters.TryAdd( optionalParameter );
                            }
                        }
                    }
                }

                result = header;
            }

            return result != null;
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

            if ( OnlyIfCached )
            {
                builder.Append( "only-if-cached, " );
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

            if ( MaximumStale.HasValue )
            {
                builder.AppendFormat( "max-stale={0}, " , MaximumStale );
            }

            if ( MinimumFresh.HasValue )
            {
                builder.AppendFormat( "min-fresh={0}, " , MinimumFresh );
            }

            if ( StaleIfError.HasValue )
            {
                builder.AppendFormat( "stale-if-error={0}, " , StaleIfError );
            }

            foreach ( var extension in Parameters )
            { 
                if ( long.TryParse( extension.Value , out var _ ) )
                {
                    builder.AppendFormat( "{0}={1}, " , extension.Name , extension.Value );
                }
                else if ( double.TryParse( extension.Value , NumberStyles.Float, CultureInfo.InvariantCulture , out var _ ) )
                {
                    builder.AppendFormat( "{0}={1} ," , extension.Name , extension.Value );
                }
                else
                {
                    builder.AppendFormat( "{0}=\"{1}\" ," , extension.Name , extension.Value );
                }
            }

            return builder.ToString().Trim( ' ' , ',' );
        }
    }
}
