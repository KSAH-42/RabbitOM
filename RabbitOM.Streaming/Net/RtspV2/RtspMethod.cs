using System;
using System.Reflection;

namespace RabbitOM.Streaming.Net.RtspV2
{
    public sealed class RtspMethod
    {

        private RtspMethod( string name ) => Name = name;
        
        public string Name { get; }



        public static RtspMethod Null { get; } = new RtspMethod( string.Empty );
        public static RtspMethod Options { get; } = new RtspMethod( "OPTIONS" );
        public static RtspMethod Describe { get; } = new RtspMethod( "DESCRIBE" );
        public static RtspMethod Setup { get; } = new RtspMethod( "SETUP" );
        public static RtspMethod Play { get; } = new RtspMethod( "PLAY" );
        public static RtspMethod Pause { get; } = new RtspMethod( "PAUSE" );
        public static RtspMethod Teardown { get; } = new RtspMethod( "TEARDOWN" );
        public static RtspMethod KeepAlive { get; } = new RtspMethod( "KEEPALIVE" );
        public static RtspMethod GetParameter { get; } = new RtspMethod( "GET_PARAMETER" );
        public static RtspMethod SetParameter { get; } = new RtspMethod( "SET_PARAMETER" );
        public static RtspMethod Announce { get; } = new RtspMethod( "ANNOUNCE" );
        public static RtspMethod Redirect { get; } = new RtspMethod( "REDIRECT" );
        public static RtspMethod Record { get; } = new RtspMethod( "RECORD" );



        public override string ToString() => Name;



        public static bool TryParse( string value , out RtspMethod result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var methodName = value.Trim().ToUpper();

            foreach ( PropertyInfo property in typeof( RtspMethod ).GetProperties( BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty ) )
            {
                var method = property.GetValue( null ) as RtspMethod ;

                if ( method != null && method.Name == methodName )
                {
                    result = method;
                    break;
                }
            }

            return result != null;
        }
    }
}
