using FFmpeg.AutoGen;
using System;

namespace RabbitOM.Decoder.FFMpeg.Infrastructure.Codecs
{
    using RabbitOM.Decoder.FFMpeg.Domain.Codecs;

    public sealed class VideoDecoderFactory : IVideoDecoderFactory
    {
        static VideoDecoderFactory()
        {
            ffmpeg.RootPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        public bool CanCreateDecoder( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return value.Contains( "H265" , StringComparison.OrdinalIgnoreCase )
                || value.Contains( "H264" , StringComparison.OrdinalIgnoreCase )
                || value.Contains( "JPEG" , StringComparison.OrdinalIgnoreCase )
                ;
        }

        public IVideoDecoder CreateDecoder( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( value.Contains( "H265" , StringComparison.OrdinalIgnoreCase ) )
            {
                return new SoftwareVideoDecoder( AVCodecID.AV_CODEC_ID_HEVC );
            }

            if ( value.Contains( "H264" , StringComparison.OrdinalIgnoreCase ) )
            {
                return new SoftwareVideoDecoder( AVCodecID.AV_CODEC_ID_H264 );
            }

            if ( value.Contains( "JPEG" , StringComparison.OrdinalIgnoreCase ) )
            {
                return new SoftwareVideoDecoder( AVCodecID.AV_CODEC_ID_MJPEG );
            }

            throw new NotSupportedException( nameof( value ) );
        }
    }
}