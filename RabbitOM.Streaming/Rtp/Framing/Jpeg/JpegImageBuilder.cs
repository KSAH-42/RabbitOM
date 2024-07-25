using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegImageBuilder
    {
        private readonly JpegStreamWriter _stream;
        private JpegFragment _sample;
        private byte[] _headers;




        public JpegImageBuilder( JpegStreamWriter stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }





        public void WriteInitialFragment( JpegFragment fragment )
        {
            if ( fragment == null )
            {
                throw new ArgumentNullException( nameof( fragment ) );
            }

            _stream.Clear();

            OnStartOfImage( fragment );

            _stream.WriteImageData( fragment.Data );
        }

        public void WriteLastFragment( JpegFragment fragment )
        {
            if ( fragment == null )
                throw new ArgumentNullException( nameof( fragment ) );

            _stream.WriteImageData( fragment.Data );

            OnEndOfImage( fragment );
        }

        public void WriteIntermediaryFragment( JpegFragment fragment )
        {
            if ( fragment == null )
                throw new ArgumentNullException( nameof( fragment ) );

            _stream.WriteImageData( fragment.Data );
        }

        public byte[] BuildImage()
        {
            return _stream.ToArray();
        }

        public void Clear()
        {
            _stream.Clear();
            _sample = null;
            _headers = null;
        }





        private void OnStartOfImage( JpegFragment fragment )
        {
            if ( ! JpegFragment.IsSimilar( fragment , _sample ) )
            {
                _sample = fragment;
                _headers = null;
            }

            if ( _headers != null )
            {
                _stream.Write( _headers );
            }
            else
            {
                _stream.WriteStartOfImage();
                _stream.WriteApplicationJFIF();
                _stream.WriteDri( fragment.Dri );
                _stream.WriteQuantizationTable( fragment.QTable );
                _stream.WriteStartOfFrame( fragment.Type , fragment.Width , fragment.Height , fragment.QTable.Count );
                _stream.WriteHuffmanDefaultTables();
                _stream.WriteStartOfScan();

                _headers = _stream.ToArray();
            }
        }

        private void OnEndOfImage( JpegFragment fragment )
        {
            _stream.WriteEndOfImage();
        }
    }
}
