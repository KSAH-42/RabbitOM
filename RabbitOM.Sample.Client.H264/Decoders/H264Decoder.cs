using FFmpeg.AutoGen;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#pragma warning disable CS0169
#pragma warning disable CS0414

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public sealed unsafe class H264Decoder : IDisposable
    {
        private AVCodec* _codec = null;
        private AVCodecContext* _context = null;
        private AVFrame* _frame = null;
        private AVFrame* _swframe = null;
        private AVPacket* _rawPacket = null;
        private AVDictionary* _options = null;
        private int _frameWidth;
        private int _frameHeight;
        private AVPixelFormat _pixelFomat = AVPixelFormat.AV_PIX_FMT_NONE;
        private WriteableBitmap _picture;
        private Int32Rect _dirtyRect;
        private byte[] _extraData = new byte[0];




        static H264Decoder()
        {
            ffmpeg.RootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
        }


        public byte[] PPS { get; set; }

        public byte[] SPS { get; set; }

        public byte[] StartCodePrefix { get; set; }

        public FrameworkElement TargetControl { get; set; }


        public unsafe void InitializeDecoder()
        {
        }

        public void Dispose()
        {
        }

        public bool Decode(byte[] buffer , byte[] spsPpsSegment )
        {
            return false;
        }

        public unsafe void Render()
        {

        }
    }
}


#pragma warning restore CS0414
#pragma warning restore CS0169