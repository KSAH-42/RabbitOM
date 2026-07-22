using System;

namespace RabbitOM.Sample.Client.H265.Codecs
{
    public abstract class H265Decoder : IDisposable
    {
        public event EventHandler<H265DecodedEventArgs> Decoded;

        ~H265Decoder()
        {
            Dispose( false );
        }

        public abstract bool IsOpened { get; }

        public abstract void Open();

        public abstract void Close();

        public abstract void Decode( byte[] buffer , H265Options options );

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }

        protected virtual void OnDecoded( H265DecodedEventArgs e )
        {
            Decoded?.Invoke( this , e );
        }
    }
}