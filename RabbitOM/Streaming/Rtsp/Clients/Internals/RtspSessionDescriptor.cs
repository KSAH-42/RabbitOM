using System;
using System.Linq;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    using RabbitOM.Streaming.Sdp;

    internal sealed class RtspClientSessionDescriptor
    {
        private readonly object _lock = new object();
        private SessionDescription _sdp;
        private RtspTrackInfo _selectedTrack;


        public object SyncRoot 
        {
            get => _lock;
        }

        public RtspTrackInfo SelectedTrack
        {
            get
            {
                lock ( _lock )
                {
                    return _selectedTrack ?? RtspTrackInfo.Empty;
                }
            }
        }

        public bool IsValid()
        {
            lock ( _lock )
            {
                return _sdp != null;
            }
        }

        public bool Extract( string text )
        {
            lock ( _lock )
            {
                if ( _sdp != null )
                {
                    return false;
                }

                _selectedTrack = null;

                return SessionDescription.TryParse( text , out _sdp );
            }
        }

        public void Clear()
        {
            lock ( _lock )
            {
                _sdp = null;
                _selectedTrack = null;
            }
        }

        public bool SelectTrack( RtspMediaFormat mediaFormat )
        {
            lock ( _lock )
            {
                _selectedTrack = null;

                if ( _sdp == null )
                {
                    return false;
                }

                MediaTrack mediaTrack = null;
                
                switch ( mediaFormat )
                {
                    case RtspMediaFormat.Audio:
                        mediaTrack = _sdp.SelectAudioMediaTracks().FirstOrDefault();
                        break;

                    case RtspMediaFormat.Video:
                        mediaTrack = _sdp.SelectVideoMediaTracks().FirstOrDefault();
                        break;
                }

                if ( mediaTrack == null )
                {
                    return false;
                }

                if ( string.IsNullOrWhiteSpace( mediaTrack.Format.SPS ) )
                { 
                    mediaTrack.Format.SPS = FormatAttributeValue.Default_H264_SPS;
                }
                
                if ( string.IsNullOrWhiteSpace( mediaTrack.Format.PPS ) )
                { 
                    mediaTrack.Format.PPS = FormatAttributeValue.Default_H264_PPS;
                }

                if ( mediaFormat == RtspMediaFormat.Audio )
                {
                    _selectedTrack = RtspTrackInfo.NewAudioTrackInfo( mediaTrack.RtpMap.PayloadType , mediaTrack.RtpMap.Encoding , mediaTrack.RtpMap.ClockRate , mediaTrack.ControlUri , mediaTrack.Format.ProfileLevelId );
                }

                if ( mediaFormat == RtspMediaFormat.Video )
                {
                    _selectedTrack = RtspTrackInfo.NewVideoTrackInfo( mediaTrack.RtpMap.PayloadType , mediaTrack.RtpMap.Encoding , mediaTrack.RtpMap.ClockRate , mediaTrack.ControlUri , mediaTrack.Format.ProfileLevelId , mediaTrack.Format.SPS , mediaTrack.Format.PPS , mediaTrack.Format.VPS );
                }

                return _selectedTrack != null;
            }
        }
    }
}
