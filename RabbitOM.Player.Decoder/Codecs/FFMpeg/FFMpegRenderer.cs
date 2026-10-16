
using System;

namespace RabbitOM.Player.Decoder.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    public sealed unsafe class FFMpegRenderer : Renderer
    {
        private SwsContext* _sws_context = null;
        private int _height;
        private int _width;
        private byte[]? _buffer;
        private readonly int[] _stride = new int[1];



        public unsafe override void Render( Surface surface )
        {
            if ( surface == null )
            {
                return;
            }

            AVFrame* pFrame = surface.Frame != IntPtr.Zero ? (AVFrame*) surface.Frame : null;

            if ( pFrame == null || pFrame->data[0] == null )
            {
                return;
            }

            if ( surface.Width != _width || surface.Height != _height )
            {
                OnSizeChanged( surface );
            }

            if ( _sws_context == null )
            {
                _sws_context = ffmpeg.sws_getContext( surface.Width , surface.Height , AVPixelFormat.AV_PIX_FMT_YUV420P , surface.Width , surface.Height , AVPixelFormat.AV_PIX_FMT_RGB24 , ffmpeg.SWS_BILINEAR , null , null , null );

                if ( _sws_context == null )
                {
                    return;
                }
            }

            if ( _buffer == null || _buffer.Length == 0 )
            {
                return;
            }

            fixed( byte* pBuffer = &_buffer[0] )
            {
                var dstData = new byte_ptrArray8();

                dstData[0] = pBuffer;

                ffmpeg.sws_scale( _sws_context , pFrame->data , pFrame->linesize , 0 , surface.Height , dstData , _stride );
            }
        }

        public override void Close()
        {
            if ( _sws_context != null )
            {
                ffmpeg.sws_freeContext( _sws_context );
	            _sws_context = null;
            }

            _buffer = null;
            _width = 0;
            _height = 0;
            _stride[0] = 0;
        }





        private void OnSizeChanged( Surface surface )
        {
            if ( _sws_context != null )
            {
                ffmpeg.sws_freeContext( _sws_context );
                _sws_context = null;
            }

            _stride[ 0 ] = surface.Width * 4;
            _width = surface.Width;
            _height = surface.Height;
            _buffer = new byte[ _height * _stride[ 0 ] ];
        }
    }
}