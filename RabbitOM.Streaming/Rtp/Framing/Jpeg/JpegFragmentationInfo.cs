using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public struct JpegFragmentationInfo : IEquatable<JpegFragmentationInfo>
    {
        public readonly static JpegFragmentationInfo Empty = new JpegFragmentationInfo(0,0,0,0,0,default);




        public JpegFragmentationInfo( int type , int width , int height , int dri , int quantizationFactor , ArraySegment<byte> quantizationTable )
		{
            Type = type;
            Width = width;
            Height = height;
            Dri = dri;
            QFactor = quantizationFactor;
            QTable = quantizationTable;
		}



		
		public int Type { get; }
        
        public int Width { get; }
        
        public int Height { get; }
        
        public int Dri { get; }

        public int QFactor { get; }

        public ArraySegment<byte> QTable { get; }




        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode() ^ Dri.GetHashCode() ^ QFactor.GetHashCode() ^ QTable.GetHashCode();
        }

		public override bool Equals( object obj )
		{
			return Equals( this , obj );
		}

        public bool Equals( JpegFragmentationInfo obj )
        {
            return Equals( this , obj );
        }




        public static bool operator ==( JpegFragmentationInfo a , JpegFragmentationInfo b )
        {
            return Equals( a , b );
        }

        public static bool operator !=( JpegFragmentationInfo a , JpegFragmentationInfo b )
        {
            return ! Equals( a , b );
        }

        public static bool Equals( JpegFragmentationInfo a , JpegFragmentationInfo b )
        {
            return a.Type == b.Type && a.Width == b.Width && a.Height == b.Height && a.Dri == b.Dri && a.QFactor == b.QFactor && a.QTable.SequenceEquals( b.QTable ) ;
        }
    }
}
