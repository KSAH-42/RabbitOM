using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    internal static class DataReceiverExtensions
    {
        public static bool TryOpen( this IDataReceiver dataReceiver )
        {
            if ( dataReceiver == null )
            {
                throw new ArgumentNullException( nameof( dataReceiver ) );
            }

            try
            {
                dataReceiver.Open();
                return true;
            }
            catch( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        public static bool TryReceive( this IDataReceiver dataReceiver , out byte[] result )
        {
            result = null;

            if ( dataReceiver == null )
            {
                throw new ArgumentNullException( nameof( dataReceiver ) );
            }

            try
            {
                var buffer = dataReceiver.Receive();

                if ( buffer?.Length > 0 )
                {
                    result = buffer;
                    return true;
                }
            }
            catch( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        public static void OnError( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
