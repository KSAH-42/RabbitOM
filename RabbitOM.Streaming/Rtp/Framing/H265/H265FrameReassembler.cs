using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameReassembler : IDisposable
    {
        private readonly Queue<ArraySegment<byte>> _payloads = new Queue<ArraySegment<byte>>();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void AddNalUnit( H265NalUnit nalUnit )
        {
            if ( nalUnit == null )
            {
                return;
            }

            if ( nalUnit.Type != NalUnitType.AGGREGATION && nalUnit.Type != NalUnitType.FRAGMENTATION )
            {
                OnAdd( nalUnit );
            }
            
            else  if ( nalUnit.Type == NalUnitType.AGGREGATION )
            {
                OnAddAggregation( nalUnit );
            }
            
            else  if ( nalUnit.Type == NalUnitType.FRAGMENTATION )
            {
                OnAddFragmentation( nalUnit );
            }

            throw new NotImplementedException();
        }







        private void OnAdd( H265NalUnit nalUnit )
        {
            _payloads.Enqueue( nalUnit.Data );
        }

        private void OnAddAggregation( H265NalUnit nalUnit )
        {
            foreach ( var segment in nalUnit.SplitData() )
            {
                _payloads.Enqueue( segment );
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
            }

            if ( fragmentation.StartBit && fragmentation.EndBit == false )
            {
            }

            if ( fragmentation.StartBit == false && fragmentation.EndBit == true )
            {
            }
        }
    } 
}