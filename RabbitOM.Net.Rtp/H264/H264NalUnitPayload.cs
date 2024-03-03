using System;

namespace RabbitOM.Net.Rtp.H264
{
    // Make some and see it is use full to pass 
    // this class as a struct

    public sealed class H264NalUnitPayload
    {
        private readonly H264NalUnit _nalUnit;

        private byte[] _data;

        private byte[] _fu_a;

        private byte[] _fu_b;

        private byte[] _stap_a;

        private byte[] _stap_b;

        private byte[] _pps;

        private byte[] _sps;




        public H264NalUnitPayload( H264NalUnit nalunit )
        {
            _nalUnit = nalunit ?? throw new ArgumentNullException( nameof( nalunit ) );
        }




        public byte[] GetAllData()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFuA()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFuB()
        {
            throw new NotImplementedException();
        }

        public byte[] GetStapA()
        {
            throw new NotImplementedException();
        }

        public byte[] GetStapB()
        {
            throw new NotImplementedException();
        }

        public byte[] GetSPS()
        {
            throw new NotImplementedException();
        }

        public byte[] GetPPS()
        {
            throw new NotImplementedException();
        }




        public bool TryGetAllData( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetFuA( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetFuB( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetStapA( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetStapB( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetSPS( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetPPS( out byte[] result )
        {
            throw new NotImplementedException();
        }




        private byte[] CreateFromBuffer( int length )
        {
            var buffer = new byte[ length ];

            Buffer.BlockCopy( _nalUnit.Buffer.Array , _nalUnit.Buffer.Offset , buffer , 0 , buffer.Length );

            return buffer;
        }
    }
}