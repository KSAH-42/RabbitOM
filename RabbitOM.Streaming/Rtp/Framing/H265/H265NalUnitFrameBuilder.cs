using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    // The following implementation is subject to change or to be removed entirely
    // A better implementation can be done

    public sealed class H265NalUnitFrameBuilder : IDisposable
    {
        private readonly H265StreamWriter _writer = new H265StreamWriter();

        public void Dispose()
        {
            _writer.Dispose();
        }

        public void Clear()
        {
            _writer.Clear();
        }

        public bool CanBuild()
        {
            throw new NotImplementedException();
        }

        public byte[] Build()
        {
            throw new NotImplementedException();
        }

        public void AddNalUnit( ArraySegment<byte> buffer )
        {
            throw new NotImplementedException();
        }










        private void OnAdd( H265NalUnit nalUnit , ArraySegment<byte> buffer )
        {    
        }

        private void OnAddPPS( H265NalUnit nalUnit , ArraySegment<byte> buffer )
        {
        }

        private void OnAddVPS( H265NalUnit nalUnit , ArraySegment<byte> buffer )
        {
        }

        private void OnAddSPS( H265NalUnit nalUnit , ArraySegment<byte> buffer )
        {
        }

        private void OnAddAggregation( H265NalUnit nalUnit , ArraySegment<byte> buffer )
        {
        }

        private void OnAddFragmentation( H265NalUnit nalUnit , ArraySegment<byte> buffer )
        {   
        }

        private void OnAddFragmentationBegin( H265NalUnit nalUnit , ArraySegment<byte> buffer )
        {
        }

        private void OnAddFragmentationEnded( H265NalUnit nalUnit , ArraySegment<byte> buffer )
        {
        }
    } 
}