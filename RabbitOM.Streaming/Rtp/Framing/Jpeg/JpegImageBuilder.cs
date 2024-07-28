using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegImageBuilder : IDisposable
    {
        private readonly JpegStreamWriter _writer = new JpegStreamWriter();
        private readonly JpegFragmentQueue _fragments = new JpegFragmentQueue();
       
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

        public bool CanBuildImage()
        {
            return _fragments.Any();
        }

        public JpegImage BuildImage()
        {
            var firstFragment = _fragments.Dequeue();

            _writer.Clear();

            _writer.WriteStartOfImage();
            _writer.WriteApplicationJFIF();
            _writer.WriteRestartInterval( firstFragment.Dri );
            _writer.WriteQuantizationTable( firstFragment.QTable , firstFragment.QFactor );
            _writer.WriteStartOfFrame( firstFragment.Type , firstFragment.Width , firstFragment.Height , firstFragment.QTable.Count );
            _writer.WriteHuffmanTables();
            _writer.WriteStartOfScan();
            _writer.Write( firstFragment.Data );

            while ( _fragments.Count > 0 )
            {
                _writer.Write( _fragments.Dequeue().Data );
            }

            _writer.WriteEndOfImage();

            return new JpegImage( _writer.ToArray() , firstFragment.Width , firstFragment.Height );
        }
    }
}
