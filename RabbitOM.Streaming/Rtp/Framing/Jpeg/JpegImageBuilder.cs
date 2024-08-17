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
        ///     <para>Some approach consist to create a byte array that contains headers values after writing data on the stream using the ToArray method and reused this buffer later.</para>
        ///     <para>Another approach is used here.</para>
        ///     <para>Here, we just saves the position on the stream when headers are created, and restore it if no changed occurs.</para>
        ///     <para>I think this approach is prefered because:</para>
        ///     <para> it saved allocation times</para>
        ///     <para> it saved allocation spaces</para>
        ///     <para> and it reused the internal array of <see cref="System.IO.MemoryStream"/> and not creating a new one.</para>
        /// </remarks>
        public JpegImage BuildImage()
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

                _headersPosition = _writer.Length;

                OnCreatedHeaders( firstFragment );
            }
            else
            {
                _writer.SetLength( _headersPosition );
            }

            while ( _fragments.Any() )
            {
                _writer.Write( _fragments.Dequeue().Payload );
            }

            _writer.WriteEndOfImage();

            return new JpegImage( _writer.ToArray() , firstFragment.Width , firstFragment.Height );
        }






        /// <summary>
        /// Occurs when the image headers need to be created
        /// </summary>
        /// <param name="fragment">the first fragment</param>
        /// <returns>returns true if the headers need to be created</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <remarks>
        ///     <para>this method throw an exception if the argument is null.</para>
        ///     <para>Because if we don't do that, it create a violation of the purpose for the returned value. we can't create headers if the fragment is null or returns any types of values in this case.</para>
        ///     <para>in others words, we can allow to execute any code for any status returns by this method.</para>
        ///     <para>an exception must be thrown</para>
        ///     <para>the null check must done in an other place</para>
        ///     <para>with the actual implementation, it can not happens but i add it just in case some change some code here, in this class.</para>
        /// </remarks>
        private bool OnCreatingHeaders( JpegFragment fragment )
        {
            if ( fragment == null )
            {
                throw new ArgumentNullException( nameof( fragment ) );
            }

            return _headersPosition               == 0
                || _firstFragment                 == null 
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