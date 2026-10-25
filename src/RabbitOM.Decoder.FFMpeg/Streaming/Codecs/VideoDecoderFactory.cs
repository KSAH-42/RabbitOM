using FFmpeg.AutoGen;
using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming.Codecs
{
    public static class VideoDecoderFactory
    {
        static VideoDecoderFactory()
        {
            ffmpeg.RootPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        public static bool IsDecoderSupported( string value )
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

        public static VideoDecoder CreateDecoder( string value )
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