using System;
using System.Linq;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Sdp
{
    /// <summary>
    /// Represent the session descriptor extension class
    /// </summary>
    public static class SessionDescriptorExtensions
    {
        /// <summary>
        /// Find the first video track uri
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>retruns a uri value</returns>
        public static string SearchVideoTrackUri( this SessionDescriptor descriptor )
        {
            return SelectVideoMediaTracks( descriptor ).ElementAtOrDefault( 0 )?.ControlUri ?? string.Empty;
        }

        /// <summary>
        /// Search all video media tracks
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>retruns a collection of media track</returns>
        public static IEnumerable<MediaTrack> SelectVideoMediaTracks( this SessionDescriptor descriptor )
        {
            return SelectMediaTracks( descriptor , element => element.Type == MediaType.Video && element.Protocol == ProtocolType.RTP );
        }

        /// <summary>
        /// Find the first audio track uri
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>retruns a uri value</returns>
        public static string SearchAudioTrackUri( this SessionDescriptor descriptor )
        {
            return SelectAudioMediaTracks( descriptor ).ElementAtOrDefault( 0 )?.ControlUri ?? string.Empty;
        }

        /// <summary>
        /// Search all audi media tracks
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>retruns a collection of media track</returns>
        public static IEnumerable<MediaTrack> SelectAudioMediaTracks( this SessionDescriptor descriptor )
        {
            return SelectMediaTracks( descriptor , element => element.Type == MediaType.Audio && element.Protocol == ProtocolType.RTP );
        }

        /// <summary>
        /// Find the first video track uri
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>retruns a uri value</returns>
        public static string SearchApplicationTrackUri( this SessionDescriptor descriptor )
        {
            return SelectApplicationTrackUri( descriptor ).ElementAt( 0 )?.ControlUri ?? string.Empty;
        }

        /// <summary>
        /// Search all video media tracks
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <returns>retruns a collection of media track</returns>
        public static IEnumerable<MediaTrack> SelectApplicationTrackUri( this SessionDescriptor descriptor )
        {
            return SelectMediaTracks( descriptor , element => element.Type == MediaType.Application && element.Protocol == ProtocolType.RTP );
        }

        /// <summary>
        /// Search all video media tracks
        /// </summary>
        /// <param name="descriptor">the descriptor</param>
        /// <param name="predicate">the predicate</param>
        /// <returns>retruns a collection of media track</returns>
        /// <exception cref="ArgumentNullException"/>
        private static IEnumerable<MediaTrack> SelectMediaTracks( this SessionDescriptor descriptor , Func<MediaDescriptionField , bool> predicate )
        {
            if ( descriptor == null )
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            foreach (var mediaDescription in descriptor.MediaDescriptions.Where(predicate))
            {
                var mediaTrack = new MediaTrack(Guid.NewGuid())
                {
                    ControlUri = mediaDescription.Attributes.FindByName( AttributeNames.Control )?.Value ?? string.Empty,
                    Address = mediaDescription.Connection.Address,
                    Port = mediaDescription.Port,
                };

                if (RtpMapAttributeValue.TryParse(mediaDescription.Attributes.FindByName( AttributeNames.RTPMap )?.Value, out RtpMapAttributeValue rtpMap))
                {
                    mediaTrack.RtpMap.CopyFrom(rtpMap);
                }

                if (FormatAttributeValue.TryParse(mediaDescription.Attributes.FindByName( AttributeNames.FormatPayload ) ?.Value, out FormatAttributeValue fmtp))
                {
                    mediaTrack.Format.CopyFrom(fmtp);
                }

                yield return mediaTrack;
            }
        }
    }
}
