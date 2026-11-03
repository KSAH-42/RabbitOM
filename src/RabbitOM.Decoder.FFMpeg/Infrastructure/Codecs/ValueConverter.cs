using FFmpeg.AutoGen;
using System;

namespace RabbitOM.Decoder.FFMpeg.Infrastructure.Codecs
{
    public static class ValueConverter
    {
        public static AVCodecID Convert( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( value.Contains( "H265" , StringComparison.OrdinalIgnoreCase ) )
            {
                return AVCodecID.AV_CODEC_ID_HEVC;
            }

            if ( value.Contains( "H264" , StringComparison.OrdinalIgnoreCase ) )
            {
                return AVCodecID.AV_CODEC_ID_H264;
            }

            if ( value.Contains( "JPEG" , StringComparison.OrdinalIgnoreCase ) )
            {
                return AVCodecID.AV_CODEC_ID_MJPEG;
            }

            throw new NotSupportedException( nameof( value ) );
        }
    }
}