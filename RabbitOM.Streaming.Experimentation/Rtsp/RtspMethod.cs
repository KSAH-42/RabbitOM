using System;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspMethod
    {
        private RtspMethod( string name ) => Name = name;
        
        public string Name { get; }
       
        public override string ToString() => Name;






        public static RtspMethod OPTIONS { get; }  =new RtspMethod( "OPTIONS" );
        
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




        public static bool Equals( RtspMethod method , string name )
        {
            if ( method == null || string.IsNullOrWhiteSpace( name ) )
            {
                return false;
            }

            return string.Equals( method.Name , name?.Trim() , StringComparison.OrdinalIgnoreCase );
        }


        

        public static bool TryParse( string value , out RtspMethod result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var methodName = value.Trim();

            foreach ( PropertyInfo property in typeof( RtspMethod ).GetProperties( BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty ) )
            {
                var method = property.GetValue( null ) as RtspMethod ;

                if ( method == null )
                {
                    continue;
                }

                if ( ! string.Equals( method.Name , methodName , StringComparison.OrdinalIgnoreCase ) )
                {
                    continue;
                }
                
                result = method;
                break;
            }

            return result != null;
        }
    }
}
