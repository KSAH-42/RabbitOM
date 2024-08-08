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

        private JpegFragment _firstFragment;

        private long _headersPosition = 0;






        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _writer.Dispose();
        }

        /// <summary>
        /// Removes all fragments
        /// </summary>
        public void Clear()
        {
            _fragments.Clear();
            _firstFragment = null;
            _headersPosition = 0;
        }

        /// <summary>
        /// Call this method before adding any fragements
        /// </summary>
        public void Initialize()
        {
            _fragments.Clear();
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
        /// Check if the fragment can be added
        /// </summary>
        /// <param name="fragment">the fragment</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanAddFragment( JpegFragment fragment )
        {
            return fragment != null && fragment.TryValidate();
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
        /// <remarks>
        ///     <para>For optimization reasons, this method don't recreate headers.</para>
        ///     <para>Some approach consist to a byte array that contains headers values after write data on the stream using the ToArray method.</para>
        ///     <para>Another approach is used here.</para>
        ///     <para>We saves the position on the stream, and restore it if no changed occurs.</para>
        ///     <para>This approach is prefered because:</para>
        ///     <para> it save allocation times</para>
        ///     <para> it save allocation memory</para>
        ///     <para> and it reused the internal array of <see cref="System.IO.MemoryStream"/>.</para>
        /// </remarks>
        public JpegImage BuildImage()
        {
            var firstFragment = _fragments.Dequeue();

            if ( OnBuildHeaders( firstFragment ) )
            {
                _firstFragment = firstFragment;

                _writer.Clear();
                
                _writer.WriteStartOfImage();
                _writer.WriteApplicationJFIF();
                _writer.WriteRestartInterval( firstFragment.Dri );
                _writer.WriteQuantizationTable( firstFragment.QTable , firstFragment.QFactor );
                _writer.WriteStartOfFrame( firstFragment.Type , firstFragment.Width , firstFragment.Height , firstFragment.QTable , firstFragment.QFactor );
                _writer.WriteHuffmanTables();
                _writer.WriteStartOfScan();

                _headersPosition = _writer.Length;
            }
            else
            {
			    _writer.SetLength( _headersPosition );
            }

            _writer.Write( firstFragment.Data );

            while ( _fragments.Count > 0 )
            {
                _writer.Write( _fragments.Dequeue().Data );
            }

            _writer.WriteEndOfImage();

            return new JpegImage( _writer.ToArray() , firstFragment.Width , firstFragment.Height );
        }






        /// <summary>
        /// Occurs when the image headers need to be created
        /// </summary>
        /// <param name="fragment">the first fragment</param>
        /// <returns>returns true if the headers need to be created</returns>
        private bool OnBuildHeaders( JpegFragment fragment )
        {
            if ( _headersPosition == 0 )
            {
                return true;
            }

            if ( _firstFragment == null || fragment == null )
            {
                return true;
            }

            return _firstFragment.Type    != fragment.Type 
                || _firstFragment.QFactor != fragment.QFactor
                || _firstFragment.Width   != fragment.Width
                || _firstFragment.Height  != fragment.Height
                || _firstFragment.Dri     != fragment.Dri
                || _firstFragment.QTable.IsEqualTo( fragment.QTable ) == false
                ;
        }
    }
}
