using System;

namespace RabbitOM.Net.Rtp.Mjpeg
{
    /// <summary>
    /// Represent the media payload parser
    /// </summary>
    [Obsolete]
    public abstract class MediaPayloadParser : IDisposable
    {
        /// <summary>
        /// Occurs when a frame has been received
        /// </summary>
        public event EventHandler<RtpFrameReceivedEventArgs> FrameReceived;




        /// <summary>
        /// The finalizer
        /// </summary>
        ~MediaPayloadParser()
        {
            Dispose( false );
        }




        /// <summary>
        /// Parse the data
        /// </summary>
        /// <param name="timeOffset">the time stamp</param>
        /// <param name="packet">the packet</param>
        public abstract void Parse( TimeSpan timeOffset , RtpPacket packet );

        /// <summary>
        /// Reset
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">the disposing</param>
        protected abstract void Dispose( bool disposing );




        /// <summary>
        /// Try Get time stampe
        /// </summary>
        /// <param name="baseTimeStamp">the base timestape</param>
        /// <param name="timeOffset">the time offset</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryResolveTimestamp( ref DateTime baseTimeStamp , TimeSpan timeOffset , out DateTime result )
        {
            result = DateTime.UtcNow;

            if ( baseTimeStamp == DateTime.MinValue )
            {
                return false;
            }

            if ( timeOffset == TimeSpan.MinValue )
            {
                return false;
            }

            result = baseTimeStamp + timeOffset;

            return true;
        }





        /// <summary>
        /// Occurs when the frame has been parsed
        /// </summary>
        /// <param name="e">the event args</param>
        protected virtual void OnFrameReceived( RtpFrameReceivedEventArgs e )
        {
            if ( e == null )
            {
                return;
            }

            FrameReceived?.Invoke( this , e );
        }
    }
}
