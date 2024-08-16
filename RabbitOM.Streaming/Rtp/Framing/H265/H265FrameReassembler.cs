using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    // The following implementation is subject to change or to be removed entirely
    // A better implementation can be done

    public sealed class H265FrameReassembler : IDisposable
    {
        private readonly H265StreamWriter _writer = new H265StreamWriter();

        private readonly H265ElementQueue _elements = new H265ElementQueue();





        public void AddNalUnit( RtpPacket packet )
        {
            if ( packet == null )
            {
                return;
            }

            if ( ! H265NalUnit.TryParse( packet.Payload , out H265NalUnit nalu ) )
            {
                return;
            }

            if ( nalu.TryValidate() )
            {
                _elements.Enqueue( new H265Element( packet , nalu ) );
            }
        }

        public byte[] Reassamble()
        {
            _writer.Clear();

            while ( _elements.Any() )
            {
                H265Element element = _elements.Dequeue();

                if ( element.NalUnit.Type == NalUnitType.AGGREGATION )
                {
                    OnAddAggregation( element );
                }
                
                else if ( element.NalUnit.Type == NalUnitType.FRAGMENTATION )
                {
                    OnAddFragmentation( element );
                }

                else if ( element.NalUnit.Type == NalUnitType.PPS )
                {
                    OnAddPPS( element );
                }

                else if ( element.NalUnit.Type == NalUnitType.SPS )
                {
                    OnAddSPS( element );
                }

                else if ( element.NalUnit.Type == NalUnitType.VPS )
                {
                    OnAddVPS( element );
                }
                else
                {
                    OnAdd( element );
                }
            }

            return _writer.ToArray();
        }

        public void Clear()
        {
            _elements.Clear();

            _writer.Clear();
        }

        public void Dispose()
        {
            _elements.Clear();

            _writer.Dispose();
        }









        private void OnAdd( H265Element element )
        {    
        }

        private void OnAddPPS( H265Element element )
        {
        }

        private void OnAddVPS( H265Element element )
        {
        }

        private void OnAddSPS( H265Element element )
        {
        }

        private void OnAddAggregation( H265Element element )
        {
        }

        private void OnAddFragmentation( H265Element element )
        {   
        }
    } 
}