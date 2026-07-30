using System;

namespace RabbitOM.Sample.Client.Player.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    public static class H265DecoderTypeConverter
    {
        public static AVCodecID ConvertTo( H265DecoderType type )
        {
            if ( type == H265DecoderType.MJPEG )
            {
                return AVCodecID.AV_CODEC_ID_MJPEG;
            }

            if ( type == H265DecoderType.H264 )
            {
                return AVCodecID.AV_CODEC_ID_H264;
            }

            if ( type == H265DecoderType.H265 )
            {
                return AVCodecID.AV_CODEC_ID_HEVC;
            }

            throw new NotSupportedException();
        }
    }
}