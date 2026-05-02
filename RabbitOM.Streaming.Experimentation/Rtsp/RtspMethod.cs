using System;
using System.Collections.Generic;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    // we don't use is to to have the save code for string conversion in one place
    // and we can't compare to enum to a string where a rtsp is mainly based string

    public sealed class RtspMethod
    {
        private static readonly Lazy<IReadOnlyDictionary<string,RtspMethod>> s_methods = new Lazy<IReadOnlyDictionary<string, RtspMethod>>( () =>
        {
            var methods = new Dictionary<string,RtspMethod>( StringComparer.OrdinalIgnoreCase );

            foreach ( var property in typeof( RtspMethod ).GetFields( BindingFlags.Public | BindingFlags.Static ) )
            {
                var method = property.GetValue( null ) as RtspMethod ;

                if ( method != null )
                {
                    methods[ method.Value ] = method;
                }
            }

            return methods;
        });




        public static RtspMethod OPTIONS { get; } = new RtspMethod( "OPTIONS" );
        
        public static RtspMethod DESCRIBE { get; } = new RtspMethod( "DESCRIBE" );
        
        public static RtspMethod SETUP { get; } = new RtspMethod( "SETUP" );
        
        public static RtspMethod PLAY { get; } = new RtspMethod( "PLAY" );
        
        public static RtspMethod PAUSE { get; } = new RtspMethod( "PAUSE" );
        
        public static RtspMethod TEARDOWN { get; } = new RtspMethod( "TEARDOWN" );
        
        public static RtspMethod GET_PARAMETER { get; } = new RtspMethod( "GET_PARAMETER" );
        
        public static RtspMethod SET_PARAMETER { get; } = new RtspMethod( "SET_PARAMETER" );
        
        public static RtspMethod ANNOUNCE { get; } = new RtspMethod( "ANNOUNCE" );
        
        public static RtspMethod REDIRECT { get; } = new RtspMethod( "REDIRECT" );
        
        public static RtspMethod RECORD { get; } = new RtspMethod( "RECORD" );






        private RtspMethod( string value )
        {
            Value = value;
        }
        
        public string Value
        {
            get;
        }
       
        public override string ToString()
        {
            return Value;
        }




        
        public static bool Equals( RtspMethod method , string name )
        {
            if ( method == null || string.IsNullOrWhiteSpace( name ) )
            {
                return false;
            }

            return StringComparer.OrdinalIgnoreCase.Equals( method.Value , name?.Trim() );
        }

        public static bool TryParse( string value , out RtspMethod result )
        {
            return s_methods.Value.TryGetValue( value , out result );
        }
    }
}
