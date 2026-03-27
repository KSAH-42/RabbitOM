using System;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class MethodHeaderValue
    {
        private MethodHeaderValue( string name ) => Name = name;
        



        public string Name { get; }
       
        public override string ToString() => Name;






        public static MethodHeaderValue OPTIONS { get; }  =new MethodHeaderValue( "OPTIONS" );
        
        public static MethodHeaderValue DESCRIBE { get; } = new MethodHeaderValue( "DESCRIBE" );
        
        public static MethodHeaderValue SETUP { get; } = new MethodHeaderValue( "SETUP" );
        
        public static MethodHeaderValue PLAY { get; } = new MethodHeaderValue( "PLAY" );
        
        public static MethodHeaderValue PAUSE { get; } = new MethodHeaderValue( "PAUSE" );
        
        public static MethodHeaderValue TEARDOWN { get; } = new MethodHeaderValue( "TEARDOWN" );
        
        public static MethodHeaderValue GET_PARAMETER { get; } = new MethodHeaderValue( "GET_PARAMETER" );
        
        public static MethodHeaderValue SET_PARAMETER { get; } = new MethodHeaderValue( "SET_PARAMETER" );
        
        public static MethodHeaderValue ANNOUNCE { get; } = new MethodHeaderValue( "ANNOUNCE" );
        
        public static MethodHeaderValue REDIRECT { get; } = new MethodHeaderValue( "REDIRECT" );
        
        public static MethodHeaderValue RECORD { get; } = new MethodHeaderValue( "RECORD" );




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

            foreach ( var property in typeof( MethodHeaderValue ).GetProperties( BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty ) )
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
