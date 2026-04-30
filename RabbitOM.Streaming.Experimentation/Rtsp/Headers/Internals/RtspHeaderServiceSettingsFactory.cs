using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderServiceSettingsFactory
    {
        public static RtspHeaderServiceSettings CreateServiceSettingsForRequests()
        {
            throw new NotImplementedException();
        }

        public static RtspHeaderServiceSettings CreateServiceSettingsForResponses()
        {
            throw new NotImplementedException();
        }




        private static RtspHeaderParser CreateParser<T>( string name , TryParseDelegate<T> tryParseDelegate )
        {
            return new RtspHeaderParser( name , new TryParseDelegate<object>( (string input, out object result ) =>
            {
                result = null;

                if ( tryParseDelegate( input , out T parseResult ) )
                {
                    result = parseResult;
                    return true;
                }

                return false;
            } ) );
        }
    }
}
