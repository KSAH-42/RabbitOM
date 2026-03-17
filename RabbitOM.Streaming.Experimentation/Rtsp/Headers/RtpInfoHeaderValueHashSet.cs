using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal class RtpInfoHeaderValueHashSet : RtspHeaderValueCollection<RtpInfo>
    {
        private readonly HashSet<RtpInfo> _collection = new HashSet<RtpInfo>();




        public override object SyncRoot { get => _collection; }

        public override int Count { get => _collection.Count; }

        public override bool IsSynchronized { get => false; }

        public override bool IsReadOnly { get => false; }





        public override void Add( RtpInfo item )
        {
            if ( item == null )
            {
                throw new ArgumentNullException( nameof( item ) );
            }

            if ( ! _collection.Add( item ) )
            {
                throw new ArgumentException( nameof( item ) );
            }
        }

        public override void Clear()
        {
            _collection.Clear();
        }

        public override bool Contains( RtpInfo item )
        {
            return _collection.Contains( item );
        }

        public override void CopyTo( Array array , int index )
        {
            _collection.CopyTo( array as RtpInfo[] , index );
        }

        public override void CopyTo( RtpInfo[] array , int arrayIndex )
        {
            _collection.CopyTo( array , arrayIndex );
        }

        public override bool Remove( RtpInfo item )
        {
            return _collection.Remove( item );
        }

        public override bool TryAdd( RtpInfo item )
        {
            if ( item == null )
            {
                return false;
            }

            return _collection.Add( item );
        }

        public override bool TryParseWithAdd( string input )
        {
            return RtpInfo.TryParse( input , out var element ) 
                ? TryAdd( element ) 
                : false
                ;
        }

        public override string ToString()
        {
            return string.Join( ", " , _collection );
        }
        
        protected override IEnumerator<RtpInfo> BaseGetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
