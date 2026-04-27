using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H266
{
    using RabbitOM.Streaming.Net.Rtp.H266.Payloads;

    public sealed class H266FrameFactory : IDisposable
    {
        private readonly H266StreamWriter _writer = new H266StreamWriter();





        public byte[] SPS
        {
            get => _writer.Settings.SPS;
            set => _writer.Settings.SPS = value;
        }

        public byte[] PPS
        {
            get => _writer.Settings.PPS;
            set => _writer.Settings.PPS = value;
        }

        public byte[] VPS
        {
            get => _writer.Settings.VPS;
            set => _writer.Settings.VPS = value;
        }

        public bool DONL
        {
            get => _writer.Settings.DONL;
            set => _writer.Settings.DONL = value;
        }






        public bool TryCreate( IEnumerable<RtpPacket> packets , out H266MediaElement result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.Clear();

            foreach ( var packet in packets )
            {
                if ( H266Payload.TryParse( packet.Payload , out var payload ) )
                {
                    switch ( payload.Type )
                    {
                        case H266PayloadType.PPS: 
                            _writer.WritePPS( packet ); 
                            break;

                        case H266PayloadType.SPS: 
                            _writer.WriteSPS( packet ); 
                            break;

                        case H266PayloadType.VPS: 
                            _writer.WriteVPS( packet ); 
                            break;

                        case H266PayloadType.RSV_NVCL_28: 
                            _writer.WriteAggregation( packet ); 
                            break;

                        case H266PayloadType.RSV_NVCL_29: 
                            _writer.WriteFragmentation( packet ); 
                            break;

                        default:

                            if ( H266Payload.IsSlice( payload ) )
                            {
                                _writer.Write( packet );
                            }

                            break;
                    }
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.TryValidate() )
            {
                result = new H266MediaElement( _writer.ToArray() 
                    , RtpStartCodePrefix.Default 
                    , _writer.Settings.PPS 
                    , _writer.Settings.SPS 
                    , _writer.Settings.VPS );
            }

            return result != null;
        }

        public void Clear()
        {
            _writer.Clear();
            _writer.Settings.Clear();
        }
        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
