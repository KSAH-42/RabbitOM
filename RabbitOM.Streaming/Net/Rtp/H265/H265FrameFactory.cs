using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represente the H265 frame factory
    /// </summary>
    public sealed class H265FrameFactory : IDisposable
    {
        private readonly H265FrameBuilderConfiguration _configuration;

        private readonly H265StreamWriter _writer;






        /// <summary>
        /// Initialize a new instance of the H265 frame factory
        /// </summary>
        /// <param name="configuration">the configuration object</param>
        /// <exception cref="ArgumentNullException"/>
        public H265FrameFactory( H265FrameBuilderConfiguration configuration )
        {
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );

            _writer = new H265StreamWriter();
        } 





        /// <summary>
        /// Try to create the frame
        /// </summary>
        /// <param name="packets">the aggregated packets collection</param>
        /// <param name="result">the output frame</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryCreateFrame( IEnumerable<RtpPacket> packets , out RtpFrame result )
        {
            result = null;

            if ( packets == null )
            {
                return false;
            }

            _writer.SetLength( 0 );
            
            H265StreamWriterSettings.AssignParameters( _writer.Settings , _configuration.PPS , _configuration.SPS , _configuration.VPS );
     
            foreach ( var packet in packets )
            {
                if ( H265NalUnit.TryParse( packet.Payload , out var nalUnit ) )
                {
                    if ( H265NalUnit.IsInvalidOrUnDefined( ref nalUnit ) )
                    {
                        continue;
                    }
                    
                    switch ( nalUnit.Type )
                    {
                        case H265NalUnitType.PPS: 
                            _writer.WritePPS( packet ); 
                            break;

                        case H265NalUnitType.SPS: 
                            _writer.WriteSPS( packet ); 
                            break;

                        case H265NalUnitType.VPS: 
                            _writer.WriteVPS( packet ); 
                            break;

                        case H265NalUnitType.AGGREGATION: 
                            _writer.WriteAggregation( packet ); 
                            break;

                        case H265NalUnitType.FRAGMENTATION: 
                            _writer.WriteFragmentation( packet ); 
                            break;

                        default:
                            _writer.Write( packet );
                            break;
                    }
                }
            }

            if ( _writer.Length > 0 && _writer.Settings.TryValidate() )
            {
                result = new H265Frame( _writer.ToArray() , _writer.Settings.PPS , _writer.Settings.SPS , _writer.Settings.VPS , _writer.Settings.CreateParamsBuffer() );
            }

            return result != null;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _writer.Clear();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _writer.Clear();
            _writer.Dispose();
        }
    }
}
