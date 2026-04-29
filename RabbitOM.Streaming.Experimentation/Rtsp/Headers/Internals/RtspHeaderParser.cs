using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal delegate bool TryParseDelegate<T>(string input, out T result);

    internal sealed class RtspHeaderParser
    {
        private readonly string _name;
        
        private readonly TryParseDelegate<object> _parseDelegate;




        public RtspHeaderParser( string name , TryParseDelegate<object> parseDelegate )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            if ( parseDelegate == null )
            {
                throw new ArgumentNullException( nameof( parseDelegate ) );
            }

            _name = name;
            _parseDelegate = parseDelegate;
        }




        public string Name
        {
            get => _name;
        }

        public bool TryParse(string input, out object result)
        {
            return _parseDelegate( input , out result );
        }
    }
}
