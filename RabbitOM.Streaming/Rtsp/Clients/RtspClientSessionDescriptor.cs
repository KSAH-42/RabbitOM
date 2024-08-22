using RabbitOM.Streaming.Codecs;
using RabbitOM.Streaming.Sdp;
using RabbitOM.Streaming.Sdp.Extensions;
using System;
using System.Linq;

namespace RabbitOM.Streaming.Rtsp.Clients
{    
    /// <summary>
    /// Represent the client session descriptor
    /// </summary>
    internal sealed class RtspClientSessionDescriptor
    {
        private readonly object     _lock                = new object();

        private SessionDescriptor   _sdp                 = null; 

        private RtspTrackInfo       _selectedTrack       = RtspTrackInfo.Empty;
        


        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot 
        { 
            get => _lock;
        }

        /// <summary>
        /// Gets the selected track info
        /// </summary>
        public RtspTrackInfo SelectedTrack
        {
            get
            {
                lock ( _lock )
                {
                    return _selectedTrack;
                }
            }
        }
        


        /// <summary>
        /// Check if the sdp has been created
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool IsValid()
        {
            lock ( _lock )
            {
                return _sdp != null;
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="text">the text</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Extract( string text )
        {
            lock ( _lock )
            {
                if ( _sdp != null )
                {
                    return false;
                }

                _selectedTrack = RtspTrackInfo.Empty;

                return SessionDescriptor.TryParse( text , out _sdp ) && _sdp != null;
            }
        }

        /// <summary>
        /// Release internal members
        /// </summary>
        public void Release()
        {
            lock ( _lock )
            {
                _sdp = null;
                _selectedTrack = RtspTrackInfo.Empty;
            }
        }

        /// <summary>
        /// Select the media track
        /// </summary>
        /// <param name="mediaFormat">the media format</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SelectTrack( RtspMediaFormat mediaFormat )
        {
            lock ( _lock )
            {
                _selectedTrack = RtspTrackInfo.Empty;

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

                // TODO: This section need to be refactored: It is probably better to create an base class and a derivated class to different encoding settings
                // mediaTrack.Encoding as MJPEGEncoding or as H264Encoding
                // Perhpas include SPS or PPS into the configuration class
                // client.Configuration.Encoding.SPS
                // client.Configuration.Encoding.PPS

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
                    _selectedTrack = RtspTrackInfo.NewVideoTrackInfo( mediaTrack.RtpMap.PayloadType , mediaTrack.RtpMap.Encoding , mediaTrack.RtpMap.ClockRate , mediaTrack.ControlUri , mediaTrack.Format.ProfileLevelId , mediaTrack.Format.SPS , mediaTrack.Format.PPS );
                }

                if ( _selectedTrack != null )
                {
                    return true;
                }

                _selectedTrack = RtspTrackInfo.Empty;

                return false;
            }
        }
    }
}
