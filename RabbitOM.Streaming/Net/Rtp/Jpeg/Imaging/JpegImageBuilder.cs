using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging
{
    using RabbitOM.Streaming.Net.Rtp.Jpeg.Imaging.Fragmentation;
    
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
        /// <exception cref="ArgumentNullException"/>
        public void AddFragment( JpegFragment fragment )
        {
            _fragments.Enqueue( fragment ?? throw new ArgumentNullException( nameof( fragment ) ) );
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
        public bool CanBuildFrame()
        {
            return _fragments.Count > 0;
        }

        /// <summary>
        /// Build the frame from the fragments
        /// </summary>
        /// <returns>returns an instance</returns>
        /// <remarks>
        ///     <para>For optimization reasons, this method don't recreate headers.</para>
        ///     <para>Some approach consist to create a byte array that contains headers values after writing data on the stream using the ToArray method and reused this buffer later.</para>
        ///     <para>Another approach is used here.</para>
        ///     <para>Here, we just saves the position on the stream when headers are created, and restore it if no changed occurs.</para>
        ///     <para>I think this approach is prefered because:</para>
        ///     <para> it saved allocation times</para>
        ///     <para> it saved allocation spaces</para>
        ///     <para> and it reused the internal array of <see cref="System.IO.MemoryStream"/> and not creating a new one.</para>
        /// </remarks>
        public JpegFrame BuildFrame()
        {
            var firstFragment = _fragments.Peek();

            if ( OnCreatingHeaders( firstFragment ) )
            {
                _writer.Clear();

                _writer.WriteStartOfImage();
                _writer.WriteApplicationJFIF();
                _writer.WriteRestartInterval( firstFragment.RestartInterval );
                _writer.WriteQuantizationTable( firstFragment.QTable , firstFragment.QFactor );
                _writer.WriteStartOfFrame( firstFragment.Type , firstFragment.Width , firstFragment.Height , firstFragment.QTable , firstFragment.QFactor );
                _writer.WriteHuffmanTables();
                _writer.WriteStartOfScan();

                _headersPosition = _writer.Position;

                OnCreatedHeaders( firstFragment );
            }
            else
            {
                _writer.SetPosition( _headersPosition );
            }

            while ( _fragments.Count > 0 )
            {
                _writer.WriteData( _fragments.Dequeue().Payload );
            }

            _writer.WriteEndOfImage();

            return new JpegFrame( _writer.ToArray() , firstFragment.Width , firstFragment.Height );
        }






        /// <summary>
        /// Occurs when the image headers need to be renew. If the first fragment doesn't changed there is no way to recreated jpeg headers.
        /// </summary>
        /// <returns>returns true if the headers need to be created</returns>
        private bool OnCreatingHeaders( JpegFragment fragment )
        {
            System.Diagnostics.Debug.Assert( fragment != null );

            return _headersPosition               == 0
                || _firstFragment.Type            != fragment.Type 
                || _firstFragment.Width           != fragment.Width
                || _firstFragment.Height          != fragment.Height
                || _firstFragment.RestartInterval != fragment.RestartInterval
                || _firstFragment.QFactor         != fragment.QFactor
                || _firstFragment.QTable.IsEqualTo( fragment.QTable ) == false
                ;
        }

        /// <summary>
        /// Occurs when the headers has been created
        /// </summary>
        /// <param name="firstFragment">the first fragment</param>
        private void OnCreatedHeaders( JpegFragment firstFragment )
        {
            _firstFragment = firstFragment;
        }
    }
}