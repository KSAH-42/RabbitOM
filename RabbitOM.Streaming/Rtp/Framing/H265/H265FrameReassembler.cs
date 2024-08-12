using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
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

        public bool CanAddNalUnit( H265NalUnit nalUnit )
        {
            return nalUnit != null && nalUnit.TryValidate();
        }

        public void AddNalUnit( H265NalUnit nalUnit )
        {
            if ( nalUnit == null )
            {
                throw new ArgumentNullException( nameof( nalUnit ) );
            }

            if ( nalUnit.Type != NalUnitType.AGGREGATION && nalUnit.Type != NalUnitType.FRAGMENTATION )
            {
                OnAdd( nalUnit );
            }
            
            else if ( nalUnit.Type == NalUnitType.AGGREGATION )
            {
                OnAddAggregation( nalUnit );
            }
            
            else if ( nalUnit.Type == NalUnitType.FRAGMENTATION )
            {
                OnAddFragmentation( nalUnit );
            }
            else
            {
                // the object parameter seems to be not validated, so just to alert that something wrong happen by we throwing an exception
                // and to alert that the CanAddNalUnit method seems to be not called
                // so just for alerting that a validation must be done before calling this method
                // and to avoid to call twice the nalUnit.TryValidate method
                // we throw this exception at the end of this method
                throw new ArgumentException( nameof( nalUnit ) );
            }
        }








        private void OnAdd( H265NalUnit nalUnit )
        {
            _elements.Enqueue( H265NalElement.NewNalElement( nalUnit.Data ) );
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