using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;
using System.Xml.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspMethod
    {
        private readonly string _procedureName;







        public RtspMethod( string procedureName )
        {
            if ( procedureName == null )
            {
                throw new ArgumentNullException( nameof( procedureName ) );
            }

            if ( ! IsValid( procedureName ) )
            {
                throw new ArgumentException( nameof( procedureName ) );
            }

            _procedureName = procedureName;
        }
        






        public string ProdecureName
        {
            get => _procedureName;
        }
       



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








        
        public static bool IsValid( string name )
        {
            if ( ! RtspHeaderValueValidator.TryEnsureWellFormedToken( name ) )
            {
                return false;
            }

            return RtspHeaderValueValidator.Contains( name , element =>
            {
                return char.IsDigit( element ) 
                    && char.IsUpper( element )
                    || char.IsDigit( element )
                    || element == '_' 
                    || element == '-' 
                    || element == '.'
                    ;
            });
        }


        public static bool TryParse( string input , out RtspMethod result )
        {
            result = IsValid( input ) ? new RtspMethod( input ) : null;

            return result != null;
        }







        public override string ToString()
        {
            return _procedureName;
        }
    }
}
