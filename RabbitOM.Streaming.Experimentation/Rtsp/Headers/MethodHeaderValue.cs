using System;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class MethodHeaderValue
    {
        public static readonly MethodHeaderValue OPTIONS = new MethodHeaderValue( "OPTIONS" );
        
        public static readonly MethodHeaderValue DESCRIBE = new MethodHeaderValue( "DESCRIBE" );
        
        public static readonly MethodHeaderValue SETUP = new MethodHeaderValue( "SETUP" );
        
        public static readonly MethodHeaderValue PLAY = new MethodHeaderValue( "PLAY" );
        
        public static readonly MethodHeaderValue PAUSE = new MethodHeaderValue( "PAUSE" );
        
        public static readonly MethodHeaderValue TEARDOWN = new MethodHeaderValue( "TEARDOWN" );
        
        public static readonly MethodHeaderValue GET_PARAMETER = new MethodHeaderValue( "GET_PARAMETER" );
        
        public static readonly MethodHeaderValue SET_PARAMETER = new MethodHeaderValue( "SET_PARAMETER" );
        
        public static readonly MethodHeaderValue ANNOUNCE = new MethodHeaderValue( "ANNOUNCE" );
        
        public static readonly MethodHeaderValue REDIRECT = new MethodHeaderValue( "REDIRECT" );
        
        public static readonly MethodHeaderValue RECORD = new MethodHeaderValue( "RECORD" );






        private MethodHeaderValue( string name ) => Name = name;
        




        public string Name { get; }
       
        public override string ToString() => Name;




        
        public static bool Equals( MethodHeaderValue method , string name )
        {
            if ( method == null || string.IsNullOrWhiteSpace( name ) )
            {
                return false;
            }

            return StringComparer.OrdinalIgnoreCase.Equals( method.Name , name?.Trim() );
        }

        public static bool TryParse( string value , out MethodHeaderValue result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var methodName = value.Trim();

            foreach ( var property in typeof( MethodHeaderValue ).GetFields( BindingFlags.Public | BindingFlags.Static ) )
            {
                var method = property.GetValue( null ) as MethodHeaderValue ;

                if ( method != null && StringComparer.OrdinalIgnoreCase.Equals( method.Name , methodName ) )
                {
                    result = method;
                    break;
                }
            }

            return result != null;
        }
    }
}
