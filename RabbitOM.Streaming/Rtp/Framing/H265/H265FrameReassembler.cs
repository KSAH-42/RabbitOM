using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    // The following implementation is subject to change or to be removed entirely

    public sealed class H265FrameReassembler : IDisposable
    {
        private readonly Queue<H265NalElement> _elements = new Queue<H265NalElement>();

        public void Dispose()
        {
            _elements.Clear();
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public void AddNalUnit( H265NalUnit nalUnit )
        {
            if ( nalUnit == null )
            {
                throw new ArgumentNullException( nameof( nalUnit ) );
            }

            if ( ! nalUnit.TryValidate() )
            {
                throw new ArgumentException( nameof( nalUnit ) );
            }

            switch ( nalUnit.Type )
            {
                case NalUnitType.AGGREGATION:
                    OnAddAggregation( nalUnit );
                    break;

                case NalUnitType.FRAGMENTATION:
                    OnAddFragmentation( nalUnit );
                    break;

                case NalUnitType.PPS:
                    OnAddPPS( nalUnit );
                    break;

                case NalUnitType.SPS:
                    OnAddSPS( nalUnit );
                    break;

                case NalUnitType.VPS:
                    OnAddVPS( nalUnit );
                    break;

                default:
                    OnAdd( nalUnit );
                    break;
            }
        }








        private void OnAdd( H265NalUnit nalUnit )
        {                                                                  
            _elements.Enqueue( H265NalElement.NewNalElement( nalUnit.Data ) );
        }

        private void OnAddPPS( H265NalUnit nalUnit )
        {
            _elements.Enqueue( H265NalElement.NewPPS( nalUnit.Data ) );
        }

        private void OnAddVPS( H265NalUnit nalUnit )
        {
            _elements.Enqueue( H265NalElement.NewVPS( nalUnit.Data ) );
        }

        private void OnAddSPS( H265NalUnit nalUnit )
        {
            _elements.Enqueue( H265NalElement.NewSPS( nalUnit.Data ) );
        }

        private void OnAddAggregation( H265NalUnit nalUnit )
        {
            foreach ( var segment in nalUnit.SplitData() )
            {
                _elements.Enqueue( H265NalElement.NewNalElement( segment , true ) );
            }
        }

        private void OnAddFragmentation( H265NalUnit nalUnit )
        {
            if ( ! FragmentationUnit.TryParse( nalUnit.Data , out FragmentationUnit fragmentation ) )
            {
                return;
            }

            if ( fragmentation.StartBit == false && fragmentation.EndBit == false )
            {
                // TODO
            }

            if ( fragmentation.StartBit && fragmentation.EndBit == false )
            {
                // TODO
            }

            if ( fragmentation.StartBit == false && fragmentation.EndBit == true )
            {
                // TODO
            }
        }
    } 
}