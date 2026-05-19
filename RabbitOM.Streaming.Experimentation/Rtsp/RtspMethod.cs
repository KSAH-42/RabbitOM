using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    public sealed class RtspMethod
    {
        private static readonly Func<char,bool> CharValidator = value => 
        {
            if ( value == ' ' )
            {
                return false;
            }

            return char.IsLetter( value ) && char.IsUpper( value )
                || char.IsDigit( value )
                || value == '_' 
                || value == '-' 
                || value == '.'
                ;
        };

        


        private readonly string _procedureName;





        public RtspMethod( string procedureName )
        {
            RtspHeaderValueValidator.EnsureWellFormedTokenAndAll( procedureName , CharValidator );
           
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






        public static bool TryParse( string input , out RtspMethod result )
        {
            result = RtspHeaderValueValidator.TryEnsureWellFormedTokenIfAll( input , CharValidator ) ? new RtspMethod( input ) : null ;

            return result != null;
        }







        public override string ToString()
        {
            return _procedureName;
        }
    }
}
