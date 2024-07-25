using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegImageBuilder
    {
        private readonly JpegStreamWriter _writer;
        private readonly Queue<JpegFragment> _fragments = new Queue<JpegFragment>();




        public JpegImageBuilder( JpegStreamWriter writer )
        {
            _writer = writer ?? throw new ArgumentNullException( nameof( writer ) );
        }




        public void AddFragment( JpegFragment fragment )
        {
            _fragments.Enqueue( fragment );
        }

        public void Clear()
        {
            _fragments.Clear();
        }

        public bool CanBuild()
        {
            return _fragments.Count > 1;
        }

        public byte[] BuildFrame()
        {
            var firstFragment = _fragments.Dequeue();

            _writer.Clear();

            _writer.WriteStartOfImage();
            _writer.WriteApplicationJFIF();
            _writer.WriteDri( firstFragment.Dri );
            _writer.WriteQuantizationTable( firstFragment.Data );
            _writer.WriteStartOfFrame( firstFragment.Type , firstFragment.Width , firstFragment.Height , firstFragment.QTable.Count );
            _writer.WriteHuffmanDefaultTables();
            _writer.WriteStartOfScan();
            _writer.WriteImageData( firstFragment.Data );

            while ( _fragments.Count > 0 )
            {
                _writer.WriteImageData( _fragments.Dequeue().Data );
            }

            _writer.WriteEndOfImage();

            return _writer.ToArray();
        }
    }
}
