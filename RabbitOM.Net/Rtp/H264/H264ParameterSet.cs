using System;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264ParameterSet
    {
        private readonly byte[] _sps;

        private readonly byte[] _pps;



        
        public H264ParameterSet( byte[] sps , byte[] pps )
        {
            if ( sps == null )
            {
                throw new ArgumentNullException( nameof( sps ) );
            }
            
            if ( sps.Length == 0 )
            {
                throw new ArgumentException( nameof( sps ) );
            }
            
            if ( pps == null )
            {
                throw new ArgumentNullException( nameof( pps ) );
            }
            
            if ( pps.Length == 0 )
            {
                throw new ArgumentException( nameof( pps ) );
            }
            
            _sps = sps;
            _pps = pps;
        }




        public byte[] SPS
        {
            get => _sps;
        }

        public byte[] PPS
        {
            get => _pps;
        }




        public byte[] ToBytes()
        {
            var result = new byte[ _sps.Length + _pps.Length ];

            Buffer.BlockCopy( _sps , 0 , result , 0 , _sps.Length );
            Buffer.BlockCopy( _pps , 0 , result , _sps.Length , _pps.Length );

            return result;
        }
    }
}