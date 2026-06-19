using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspMethod
    {
        private static readonly Lazy<IReadOnlyDictionary<string,RtspMethod>> s_knowMethods = new Lazy<IReadOnlyDictionary<string, RtspMethod>>( () =>
        {
            return typeof( RtspMethod )
                .GetProperties( System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public )
                .Select( property => property.GetValue( null ) as RtspMethod )
                .Where( method => method != null )
                .ToDictionary( method => method.MethodName )
                ;
        });

        private readonly string _procedureName;






        public RtspMethod( string procedureName )
        {
            RtspHeaderValueValidator.EnsureWellFormed( procedureName );

            _procedureName = procedureName;
        }





        public string MethodName { get => _procedureName; }

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






        public static bool TryParse( string input , out RtspMethod result )
        {
            result = null;

            if ( ! RtspHeaderValueValidator.IsWellFormed( input , RtspHeaderValueValidatorCharSet.BasicToken ) )
            {
                return false;
            }

            if ( ! s_knowMethods.Value.TryGetValue( input , out result ) )
            {
                result = new RtspMethod( input );
            }

            return result != null;
        }






        public override string ToString()
        {
            return _procedureName;
        }
    }
}
