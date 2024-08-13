using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265NalElement
    {
        public bool IsPS { get => IsVPS || IsPPS || IsSPS; }
        public bool IsVPS { get; private set; }
        public bool IsPPS { get; private set; }
        public bool IsSPS { get; private set; }
        public bool IsFragment { get; private set; }
        public bool IsAggregated { get; private set; }
        public bool IsNal { get; private set; }
        public ArraySegment<byte> Data { get; private set; }

        
        
        public static H265NalElement NewNalElement( ArraySegment<byte> data )
        {
            return new H265NalElement() { Data = data , IsNal = true };
        }

        public static H265NalElement NewNalElement( ArraySegment<byte> data , bool isAggrated )
        {
            return new H265NalElement() { Data = data , IsNal = true , IsAggregated = isAggrated };
        }

        public static H265NalElement NewPPS( ArraySegment<byte> data )
        {
            return new H265NalElement() { Data = data , IsPPS = true };
        }

        public static H265NalElement NewSPS( ArraySegment<byte> data )
        {
            return new H265NalElement() { Data = data , IsSPS = true };
        }

        public static H265NalElement NewVPS( ArraySegment<byte> data )
        {
            return new H265NalElement() { Data = data , IsVPS = true };
        }
    }
}
