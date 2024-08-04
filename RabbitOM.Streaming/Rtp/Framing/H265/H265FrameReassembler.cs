using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public class H265FrameReassembler : IDisposable
    {
        ~H265FrameReassembler()
        {
            Dispose( false );
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
        }

        public virtual void Clear()
        {
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected void Dispose( bool disposing )
        {
        }







        protected virtual void OnAdd( H265NalUnit nalUnit )
        {
        }

        protected virtual void OnAddAggregation( H265NalUnit nalUnit )
        {
        }

        protected virtual void OnAddFragmentation( H265NalUnit nalUnit )
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