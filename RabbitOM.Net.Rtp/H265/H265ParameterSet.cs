/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED
*/

using System;
using System.IO;

namespace RabbitOM.Net.Rtp.H265
{
    public sealed class H265ParameterSet
    {
        private readonly byte[] _sps;

        private readonly byte[] _pps;

        private readonly byte[] _vps;

        private readonly byte[] _bytes;



        
        public H265ParameterSet( byte[] sps , byte[] pps , byte[] vps )
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

            if ( vps == null )
            {
                throw new ArgumentNullException( nameof( vps ) );
            }

            if ( vps.Length == 0 )
            {
                throw new ArgumentException( nameof( vps ) );
            }

            _sps = sps;
            _pps = pps;
            _vps = vps;

            _bytes = new byte[ sps.Length + pps.Length + vps.Length ];

            Buffer.BlockCopy( sps , 0 , _bytes , 0 , sps.Length );
            Buffer.BlockCopy( pps , 0 , _bytes , sps.Length , pps.Length );
            Buffer.BlockCopy( vps , 0 , _bytes , sps.Length + pps.Length , vps.Length );
        }




        public byte[] SPS
        {
            get => _sps;
        }

        public byte[] PPS
        {
            get => _pps;
        }

        public byte[] VPS
        {
            get => _vps;
        }




        public byte[] ToBytes()
        {
            return _bytes;
        }
    }
}