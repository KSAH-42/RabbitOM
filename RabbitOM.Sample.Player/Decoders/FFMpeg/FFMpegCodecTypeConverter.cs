using System;

namespace RabbitOM.Sample.Client.Player.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    public static class FFMpegCodecTypeConverter
    {
        public static AVCodecID ConvertTo( CodecType type )
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
    }
}