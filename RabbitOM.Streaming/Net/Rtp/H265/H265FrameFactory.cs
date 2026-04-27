using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    using RabbitOM.Streaming.Net.Rtp.H265.Payloads;

    /// <summary>
    /// Represente the H265 frame factory
    /// </summary>
    /// <remarks>
    ///     <para>this is class is mark as internal, to force the focus of people to use builder instead of this class</para>
    /// </remarks>
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265StreamWriter _writer = new H265StreamWriter();



        /// <summary>
        /// Gets / Sets the SPS
        /// </summary>
        public byte[] SPS
        {
            get => _writer.Settings.SPS;
            set => _writer.Settings.SPS = value;
        }

        /// <summary>
        /// Gets / Sets the PPS
        /// </summary>
        public byte[] PPS
        {
            get => _writer.Settings.PPS;
            set => _writer.Settings.PPS = value;
        }

        /// <summary>
        /// Gets / Sets the VPS
        /// </summary>
        public byte[] VPS
        {
            get => _writer.Settings.VPS;
            set => _writer.Settings.VPS = value;
        }

        /// <summary>
        /// Gets / Sets the DONL
        /// </summary>
        public bool DONL
        {
            get => _writer.Settings.DONL;
            set => _writer.Settings.DONL = value;
        }





        /// <summary>
        /// Try to create the frame
        /// </summary>
        /// <param name="packets">the aggregated packets collection</param>
        /// <param name="result">the output frame</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out H265MediaElement result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.Clear();

            foreach ( var packet in packets )
            {
                if ( H265Payload.TryParse( packet.Payload , out var payload ) )
                {
                    switch ( payload.Type )
                    {
                        case H265PayloadType.VPS: 
                            _writer.WriteVPS( packet ); 
                            break;

                        case H265PayloadType.SPS: 
                            _writer.WriteSPS( packet ); 
                            break;
                        
                        case H265PayloadType.PPS: 
                            _writer.WritePPS( packet ); 
                            break;
                        
                        case H265PayloadType.AGGREGATION: 
                            _writer.WriteAggregation( packet ); 
                            break;

                        case H265PayloadType.FRAGMENTATION: 
                            _writer.WriteFragmentation( packet ); 
                            break;

                        default:

                            if ( H265Payload.IsSlice( payload ) )
                            {
                                _writer.Write( packet );
                            }

                            break;
                    }
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.TryValidate() )
            {
                result = new H265MediaElement( _writer.ToArray() 
                    , RtpStartCodePrefix.Default 
                    , _writer.Settings.VPS 
                    , _writer.Settings.SPS 
                    , _writer.Settings.PPS );
            }

            return result != null;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _writer.Clear();
            _writer.Settings.Clear();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
