using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public class JpegImageBuilder
    {
        public readonly JpegStreamWriter _stream;

      
        public JpegImageBuilder( JpegStreamWriter stream )
		{
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
		}




        public void WriteInitialFragment( JpegFragment fragment )
        {
            if ( fragment == null )
                throw new ArgumentNullException( nameof( fragment ) );

            _stream.Clear();
            
            OnStartOfImage( fragment );

            _stream.WriteImageData( fragment.Data );
        }

        public void WriteLastFragment( JpegFragment fragment )
        {
            if ( fragment == null )
                throw new ArgumentNullException( nameof( fragment ) );

            _stream.WriteImageData( fragment.Data );

            OnStopOfImage( fragment );
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
        }





        protected virtual void OnStartOfImage( JpegFragment fragment )
        {
            _stream.WriteStartOfImage();
            _stream.WriteApplicationJFIF();
            _stream.WriteDri( fragment.Dri );
            _stream.WriteQuantizationTable( fragment.QTable );
            _stream.WriteStartOfFrame( fragment.Type , fragment.Width , fragment.Height , fragment.QTable.Count );
            _stream.WriteHuffmanDefaultTables();
            _stream.WriteStartOfScan();
        }

        protected virtual void OnStopOfImage( JpegFragment fragment )
        {
            _stream.WriteEndOfImage();
        }
    }
}
