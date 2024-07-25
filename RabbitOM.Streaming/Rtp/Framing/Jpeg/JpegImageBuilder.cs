using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegImageBuilder : IDisposable
    {
        private readonly JpegStreamWriter _writer = new JpegStreamWriter();
        private readonly Queue<JpegFragment> _fragments = new Queue<JpegFragment>();


        public void Dispose()
        {
            _writer.Dispose();
            _fragments.Clear();
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

            /// according to rtp mjpeg
            /// it is not possible to have sequence that containts multiple fragment with different width and height size
            /// we can create a optimization by storing the jpeg headers inside the class used to write fragments, it could save a lot time
            /// much more than the previous projects and from different existing projects.

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
