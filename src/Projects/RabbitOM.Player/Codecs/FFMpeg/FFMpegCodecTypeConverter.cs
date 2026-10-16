using System;

namespace RabbitOM.Player.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    public static class FFMpegCodecTypeConverter
    {
        public static AVCodecID Convert( CodecType type )
        {
            if ( type == CodecType.MJPEG )
            {
                return AVCodecID.AV_CODEC_ID_MJPEG;
            }

            if ( type == CodecType.H264 )
            {
                return AVCodecID.AV_CODEC_ID_H264;
            }

            if ( type == CodecType.H265 )
            {
                return AVCodecID.AV_CODEC_ID_HEVC;
            }

            throw new NotSupportedException();
        }

        public static CodecType Convert( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return CodecType.Unknown;
            }

            if ( value.IndexOf( "H265" , StringComparison.OrdinalIgnoreCase ) >= 0 )
            {
                return CodecType.H265;
            }

            if ( value.IndexOf( "H264" , StringComparison.OrdinalIgnoreCase ) >= 0 )
            {
                return CodecType.H264;
            }

            if ( value.IndexOf( "JPEG" , StringComparison.OrdinalIgnoreCase ) >= 0 )
            {
                return CodecType.MJPEG;
            }

            return CodecType.Unknown;
        }
    }
}