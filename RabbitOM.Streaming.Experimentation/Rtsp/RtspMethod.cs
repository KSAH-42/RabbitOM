using System;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspMethod
    {
        public static readonly RtspMethod Options = new RtspMethod( "OPTIONS" );
        public static readonly RtspMethod Describe = new RtspMethod( "DESCRIBE" );
        public static readonly RtspMethod Setup = new RtspMethod( "SETUP" );
        public static readonly RtspMethod Play = new RtspMethod( "PLAY" );
        public static readonly RtspMethod Pause = new RtspMethod( "PAUSE" );
        public static readonly RtspMethod Teardown = new RtspMethod( "TEARDOWN" );
        public static readonly RtspMethod KeepAlive = new RtspMethod( "KEEPALIVE" );
        public static readonly RtspMethod GetParameter = new RtspMethod( "GET_PARAMETER" );
        public static readonly RtspMethod SetParameter = new RtspMethod( "SET_PARAMETER" );
        public static readonly RtspMethod Announce = new RtspMethod( "ANNOUNCE" );
        public static readonly RtspMethod Redirect = new RtspMethod( "REDIRECT" );
        public static readonly RtspMethod Record = new RtspMethod( "RECORD" );



        private RtspMethod( string name )
        {
            Name = name;
        }
        


        public string Name { get; }



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
