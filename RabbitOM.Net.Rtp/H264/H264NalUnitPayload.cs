using System;

namespace RabbitOM.Net.Rtp.H264
{
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

        


        public byte[] GetData()
        {
            if ( _data == null )
            {
                _data = new byte[ _nalUnit.Buffer.Count ];
                
                Buffer.BlockCopy( _nalUnit.Buffer.Array , _nalUnit.Buffer.Offset , _data , 0 , _data.Length );
            }

            return _data;
        }

        public byte[] GetFuA()
        {
            if ( _fu_a == null )
            {
                throw new NotImplementedException();
            }

            return _fu_a;
        }

        public byte[] GetFuB()
        {
            if ( _fu_b == null )
            {
                throw new NotImplementedException();
            }

            return _fu_b;
        }

        public byte[] GetStapA()
        {
            if ( _stap_a == null )
            {
                throw new NotImplementedException();
            }

            return _stap_a;
        }

        public byte[] GetStapB()
        {
            if ( _stap_b == null )
            {
                throw new NotImplementedException();
            }

            return _stap_b;
        }

        public byte[] GetSps()
        {
            if ( _sps == null )
            {
                throw new NotImplementedException();
            }

            return _sps;
        }

        public byte[] GetPps()
        {
            if ( _pps == null )
            {
                throw new NotImplementedException();
            }

            return _pps;
        }
    }
}