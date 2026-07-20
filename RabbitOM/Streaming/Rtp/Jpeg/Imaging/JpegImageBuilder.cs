using System;

namespace RabbitOM.Streaming.Rtp.Jpeg.Imaging
{
    public sealed class JpegImageBuilder : IDisposable
    {
        private readonly JpegStreamWriter _writer = new JpegStreamWriter();
        private readonly JpegFragmentQueue _fragments = new JpegFragmentQueue();
        private JpegFragment _firstFragment;
        private long _headersPosition = 0;







        public ResolutionInfo? ResolutionFallback
        {
            get => _writer.Settings.ResolutionFallback;
            set => _writer.Settings.ResolutionFallback = value;
        }






        public void Initialize()
        {
            _fragments.Clear();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }

        public bool AddFragment( JpegFragment fragment )
        {
            if ( fragment == null )
            {
                return false;
            }

            _fragments.Enqueue( fragment );

            return true;
        }

        public void Clear()
        {
            _fragments.Clear();
            _firstFragment = null;
            _headersPosition = 0;
        }

        public bool CanBuildFrame()
        {
            return _fragments.Count > 0;
        }

        public JpegMediaElement BuildFrame()
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

            return new JpegMediaElement( _writer.ToArray() , firstFragment.Width , firstFragment.Height );
        }








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

        private void OnCreatedHeaders( JpegFragment firstFragment )
        {
            _firstFragment = firstFragment;
        }
    }
}
