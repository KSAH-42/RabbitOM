using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a response deserializer
    /// </summary>
    public static class RTSPMessageResponseSerializer
    {
        /// <summary>
        /// Deserialize a response
        /// </summary>
        /// <param name="input">the input value</param>
        /// <returns>returns a response, otherwise null</returns>
        /// <remarks>
        ///   <para>Please, note that this method doesn't make any validations, this class only deserialize an object</para>
        /// </remarks>
        public static RTSPMessageResponse Deserialize( string input )
        {
            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return null;
            }

            using ( var reader = new RTSPMessageResponseReader( input ) )
            {
                if ( reader.ReadLineStatus() )
                {
                    reader.ReadHeaders();
                    reader.ReadLine();
                    reader.ReadBody();

                    var response = new RTSPMessageResponse( new RTSPMessageStatus( reader.GetStatusCode() , reader.GetStatusReason() ) , new RTSPMessageVersion( reader.GetMajorVersion() , reader.GetMinorVersion() ) );

                    foreach ( var header in reader.GetHeaders() )
                    {
                        response.Headers.Add( RTSPHeaderFactory.CreateHeader( header.Key , header.Value ) );
                    }

                    response.Body.Value = reader.GetBody();

                    return response;
                }

                return null;
            }
        }
    }
}
