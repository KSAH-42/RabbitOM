using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public abstract class H264Decoder : IDisposable
    {
        public event EventHandler<H264DecodedEventArgs> Decoded;

        ~H264Decoder()
        {
            Dispose( false );
        }

        public abstract bool IsOpened { get; }

        public abstract void Open();

        public abstract void Close();

        public abstract void Decode( byte[] buffer , H264Options options );

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }

        protected virtual void OnDecoded( H264DecodedEventArgs e )
        {
            Decoded?.Invoke( this , e );
        }
    }
}