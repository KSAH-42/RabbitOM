using FFmpeg.AutoGen;
using System;

namespace RabbitOM.Decoder.FFMpeg.Codecs
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

            return value.IndexOf( "H265" , StringComparison.OrdinalIgnoreCase ) >= 0
                || value.IndexOf( "H264" , StringComparison.OrdinalIgnoreCase ) >= 0
                || value.IndexOf( "JPEG" , StringComparison.OrdinalIgnoreCase ) >= 0
                ;
        }

        public static VideoDecoder CreateDecoder( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( value.IndexOf( "H265" , StringComparison.OrdinalIgnoreCase ) >= 0 )
            {
                return new SoftwareVideoDecoder( AVCodecID.AV_CODEC_ID_HEVC );
            }

            if ( value.IndexOf( "H264" , StringComparison.OrdinalIgnoreCase ) >= 0 )
            {
                return new SoftwareVideoDecoder( AVCodecID.AV_CODEC_ID_H264 );
            }

            if ( value.IndexOf( "JPEG" , StringComparison.OrdinalIgnoreCase ) >= 0 )
            {
                return new SoftwareVideoDecoder( AVCodecID.AV_CODEC_ID_MJPEG );
            }

            throw new NotSupportedException( nameof( value ) );
        }
    }
}