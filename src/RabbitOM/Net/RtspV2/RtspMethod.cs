using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.RtspV2
{
    using RabbitOM.Net.RtspV2.Headers;

    public sealed class RtspMethod
    {
        public RtspMethod( string value )
        {
            Value = RtspHeaderValueValidator.EnsureWellFormed( value , RtspHeaderValueValidatorCharSet.MethodToken );
        }







        public string Value { get; }

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








        public static implicit operator string ( RtspMethod method )
        {
            return method.Value;
        }







        public static bool TryParse( string input , out RtspMethod result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            if ( ! RtspHeaderValueValidator.IsWellFormed( input , RtspHeaderValueValidatorCharSet.MethodToken ) )
            {
                return false;
            }

            result = new RtspMethod( input );

            return true;
        }






        public override string ToString()
        {
            return Value;
        }
    }
}
