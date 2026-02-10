using System;
using System.Reflection;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspMethod
    {
        public static readonly RtspMethod OPTIONS = new RtspMethod( "OPTIONS" );
        public static readonly RtspMethod DESCRIBE = new RtspMethod( "DESCRIBE" );
        public static readonly RtspMethod SETUP = new RtspMethod( "SETUP" );
        public static readonly RtspMethod PLAY = new RtspMethod( "PLAY" );
        public static readonly RtspMethod PAUSE = new RtspMethod( "PAUSE" );
        public static readonly RtspMethod TEARDOWN = new RtspMethod( "TEARDOWN" );
        public static readonly RtspMethod KEEPALIVE = new RtspMethod( "KEEPALIVE" );
        public static readonly RtspMethod GET_PARAMETER = new RtspMethod( "GET_PARAMETER" );
        public static readonly RtspMethod SET_PARAMETER = new RtspMethod( "SET_PARAMETER" );
        public static readonly RtspMethod ANNOUNCE = new RtspMethod( "ANNOUNCE" );
        public static readonly RtspMethod REDIRECT = new RtspMethod( "REDIRECT" );
        public static readonly RtspMethod RECORD = new RtspMethod( "RECORD" );



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

            foreach ( FieldInfo field in typeof( RtspMethod ).GetFields( BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty ) )
            {
                var method = field.GetValue( null ) as RtspMethod ;

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
