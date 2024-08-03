using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent an image builder class
    /// </summary>
    public sealed class JpegImageBuilder : IDisposable
    {
        private readonly JpegStreamWriter _writer = new JpegStreamWriter();

        private readonly JpegFragmentQueue _fragments = new JpegFragmentQueue();
       




        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _writer.Dispose();
            _fragments.Clear();
        }

        /// <summary>
        /// Check if the fragment can be added
        /// </summary>
        /// <param name="fragment">the fragment</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanAddFragment( JpegFragment fragment )
        {
            return fragment != null && fragment.TryValidate();
        }

        /// <summary>
        /// Add a fragment
        /// </summary>
        /// <param name="fragment">the fragment</param>
        public void AddFragment( JpegFragment fragment )
        {
            _fragments.Enqueue( fragment );
        }

        /// <summary>
        /// Removes all fragments
        /// </summary>
        public void Clear()
        {
            _fragments.Clear();
        }

        /// <summary>
        /// Check if an image can be build
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanBuildImage()
        {
            return _fragments.Any();
        }

        /// <summary>
        /// Build the image from the fragments
        /// </summary>
        /// <returns>returns an instance</returns>
        public JpegImage BuildImage()
        {
            var firstFragment = _fragments.Dequeue();

            _writer.Clear();

            _writer.WriteStartOfImage();
            _writer.WriteApplicationJFIF();
            _writer.WriteRestartInterval( firstFragment.Dri );
            _writer.WriteQuantizationTable( firstFragment.QTable , firstFragment.QFactor );
            _writer.WriteStartOfFrame( firstFragment.Type , firstFragment.Width , firstFragment.Height , firstFragment.QTable , firstFragment.QFactor );
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
