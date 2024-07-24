using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegImageBuilder
    {
        public readonly JpegStreamWriter _stream;
        private JpegFragment _currentFragment;

        public JpegImageBuilder( JpegStreamWriter stream )
		{
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
		}


        public void CreateHeaders( JpegFragment fragment )
        {
            if ( fragment == null )
            {
                throw new ArgumentNullException( nameof( fragment ) );
            }

            _stream.Clear();
            _stream.WriteStartOfImage();
            _stream.WriteApplicationJFIF();
            _stream.WriteDri( fragment.Dri );
            _stream.WriteQuantizationTable( fragment.QTable );
            _stream.WriteStartOfFrame( fragment.Type , fragment.Width , fragment.Height , fragment.QTable.Count );
            _stream.WriteHuffmanDefaultTables();
            _stream.WriteStartOfScan();
        }

        public void WriteFragment( JpegFragment fragment )
        {
            if ( fragment == null )
            {
                throw new ArgumentNullException( nameof( fragment ) );
            }

            _stream.WriteImageData( fragment.Data );
        }

        public bool CanBuild( JpegFragment fragment )
        {
            return fragment != null && fragment.Offset == 0 && _stream.Length > 0;
        }

        public byte[] BuildImage()
        {
            throw new NotImplementedException();
        }
    }
}
